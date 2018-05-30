USE raw2;

DROP PROCEDURE IF EXISTS MatchAll;

DELIMITER //

CREATE PROCEDURE MatchAll(param VARCHAR(1000), pageSize int(11), pageNumber int(11), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN
	SET @p = LENGTH(param) - LENGTH(REPLACE(param, ' ', '')) + 1;
	SET @s = 'SELECT p.id,
					 (CASE WHEN p.ParentID IS NOT NULL THEN CONCAT(\'A: \' , (SELECT title FROM Posts WHERE id = p.ParentID))
						   ELSE CONCAT(\'Q: \', p.title)
					 END) AS title,  
                      p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, p.Score, p.ParentID, SUM(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from wi where word = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.id AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @s = CONCAT(@s,' GROUP BY p.id, p.Body HAVING rank =', @p,	' LIMIT ', pageSize, ' OFFSET ', pageNumber * pageSize); 
	PREPARE stmt FROM @s;
	EXECUTE stmt;
END //
DELIMITER ;

CALL MatchAll('"sql" "html" "java"', 20, 0, "'2011-01-01'", "'2018-05-17'");	


-- B2


DROP PROCEDURE IF EXISTS BestMatchRanked;

DELIMITER //

CREATE PROCEDURE BestMatchRanked(param VARCHAR(1000), pageSize int(11), pageNumber int(11), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN
	SET @s = 'SELECT p.id, 
					(CASE WHEN p.ParentID IS NOT NULL THEN CONCAT(\'A: \' , (SELECT title FROM Posts WHERE id = p.ParentID))
						   ELSE CONCAT(\'Q: \', p.title)
					 END) AS title,  
                     p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, p.Score, p.ParentID, SUM(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from wi where word = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.id AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @s = CONCAT(@s,' GROUP BY p.id, p.Body ORDER BY rank DESC LIMIT ', pageSize, ' OFFSET ', pageNumber * pageSize); 
    PREPARE stmt FROM @s;
	EXECUTE stmt;
END //

DELIMITER ;

CALL BestMatchRanked('"SQL"', 25, 0, "'2010-01-01'", "'2018-05-28'");


-- B3

SET SQL_SAFE_UPDATES = 0;

DROP TABLE IF EXISTS tfidf; 
CREATE TABLE tfidf AS SELECT id AS document, word AS term 
FROM words
WHERE word REGEXP '^[A-Za-z][A-Za-z0-9_]{1,}$' 
AND tablename = 'posts' 
AND (what='title' OR what='body') 
GROUP BY document, word;

ALTER TABLE tfidf 
	ADD ndt int(11), 
 	ADD nd int(11),
    ADD nt int(11),
 	ADD tf  decimal(32,20),
 	ADD idf decimal(32,20), 
    ADD rdt decimal(32,20);
    
DROP INDEX id_words ON words;
CREATE INDEX id_words
ON words (id, word);


CREATE INDEX document_term
ON tfidf (document, term);

-- nd
UPDATE tfidf AS t
LEFT JOIN (SELECT id, COUNT(word) AS nd
		   FROM words
		   WHERE word REGEXP '^[A-Za-z][A-Za-z0-9_]{1,}$' 
		   AND tablename = 'posts' 
		   AND (what='title' OR what='body') 
		   GROUP BY id) AS w ON w.id = t.document
SET t.nd = w.nd;

-- ndt
UPDATE tfidf AS t
LEFT JOIN (SELECT id, word, COUNT(word) AS ndt
		   FROM words
		   WHERE word REGEXP '^[A-Za-z][A-Za-z0-9_]{1,}$' 
		   AND tablename = 'posts' 
		   AND (what='title' OR what='body') 
		   GROUP BY id, word) AS w ON w.id = t.document AND w.word = t.term
SET t.ndt = w.ndt;

-- nt
UPDATE tfidf AS t
LEFT JOIN (SELECT word, COUNT(DISTINCT id) AS nt
		   FROM words
		   WHERE word REGEXP '^[A-Za-z][A-Za-z0-9_]{1,}$' 
		   AND tablename = 'posts' 
		   AND (what='title' OR what='body') 
		   GROUP BY word) AS w ON w.word = t.term
SET t.nt = w.nt;

-- tf

UPDATE tfidf
SET tf = LOG(1 + (ndt / nd));

-- idf

UPDATE tfidf
SET idf = ( 1 / nt );

-- rdt

UPDATE tfidf
SET rdt = tf * idf;



-- B4

DROP PROCEDURE IF EXISTS BestMatchWeighted;

DELIMITER //

CREATE PROCEDURE BestMatchWeighted(param VARCHAR(1000), pageSize int(11), pageNumber int(11), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN	

	SET @s = 'SELECT p.id, 
					 (CASE WHEN p.ParentID IS NOT NULL THEN CONCAT(\'A: \' , (SELECT title FROM Posts WHERE id = p.ParentID))
						   ELSE CONCAT(\'Q: \', p.title)
					 END) AS title, 
					 p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, p.Score, p.ParentID, SUM(t.rdt) rank FROM Posts p, (SELECT distinct document, rdt from tfidf where term = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct document, rdt from tfidf where term ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.document AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @S = CONCAT(@s, ' GROUP BY p.id, p.Body ORDER BY rank DESC LIMIT ', pageSize, ' OFFSET ',  pageNumber * pageSize);  	
    PREPARE stmt FROM @s;
	EXECUTE stmt;
END //

DELIMITER ;

CALL BestMatchWeighted('"sql" "html" "java"', 20, 0, "'2014-01-01'", "'2018-05-17'");



-- B5


DROP PROCEDURE IF EXISTS RankedWordList;

DELIMITER //

CREATE PROCEDURE RankedWordList(param VARCHAR(1000))
BEGIN	

	SET @s = 'SELECT word, SUM(rank) as rank FROM wi, (SELECT id, SUM(t.score) rank FROM (SELECT distinct id, 1 score from wi where word = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
	SET @s = CONCAT(@s,') t GROUP BY id) t1 WHERE wi.id = t1.id AND NOT EXISTS (SELECT word FROM Stopwords WHERE Stopwords.word = wi.word)
	GROUP BY word ORDER BY rank DESC LIMIT 50');  
	
    PREPARE stmt FROM @s;
	EXECUTE stmt;
END //

DELIMITER ;

CALL RankedWordList('"sql" "html" "java"');

-- B6

DROP PROCEDURE IF EXISTS WeightedWordList;

DELIMITER //

CREATE PROCEDURE WeightedWordList(param VARCHAR(1000))
BEGIN	

	SET @s = 'SELECT term, SUM(rank) rank FROM tfidf, (SELECT document, SUM(t.rdt) rank FROM (SELECT distinct document, rdt from tfidf where term = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct document, rdt from tfidf where term ='));
	SET @s = CONCAT(@s,') t GROUP BY document) t1 WHERE tfidf.document = t1.document GROUP BY term ORDER BY rank DESC');  
	
    PREPARE stmt FROM @s;
	EXECUTE stmt;
    -- CALL InsertSearchHistory(param);
END //

DELIMITER ;

CALL WeightedWordList('"sql" "html" "java"');


-- B7


-- B8

DROP TABLE IF EXISTS Associations;
CREATE TABLE Associations AS
SELECT w1.term AS word1, w2.term AS word2, COUNT(*) AS grade 
FROM tfidf w1, tfidf w2
WHERE w1.document = w2.document AND w1.term < w2.term
AND w1.rdt > 0.0002 AND w2.rdt > 0.0002 AND w1.nt > 20 AND w2.nt > 20
GROUP BY w1.term, w2.term
ORDER BY grade DESC;


DROP PROCEDURE IF EXISTS GetAssociations;

DELIMITER //

CREATE PROCEDURE GetAssociations(param VARCHAR(1000))
BEGIN	    
    SET @s = 'SELECT word2, SUM(grade) grade FROM (SELECT word2, grade FROM Associations WHERE word1 = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT word2, grade FROM Associations WHERE word1 ='));
	SET @s = CONCAT(@s,') t GROUP BY word2 ORDER BY grade DESC');  
	
    PREPARE stmt FROM @s;
	EXECUTE stmt;   
    
END //

DELIMITER ;

CALL GetAssociations('"SQL"');

-- B9


DROP PROCEDURE IF EXISTS term_network;

DELIMITER //

CREATE PROCEDURE term_network(IN w VARCHAR(100), n DOUBLE)
BEGIN
	SELECT 'var graph = '
	UNION
	SELECT '{"nodes":['
	UNION
	SELECT ''
	UNION
	SELECT CONCAT('{"id":"',LOWER(word2),'"},') FROM Associations WHERE word1=w AND grade>=n UNION SELECT CONCAT('{"id":"',w,'"},')
	UNION
	SELECT '],'
	UNION
	SELECT '"links":['
	UNION
	SELECT CONCAT('{"source":"',LOWER(word1),'", "target":"', LOWER(word2),'", "value":',grade/2,'},') line FROM (
	SELECT * FROM Associations WHERE word1 = w AND grade>=n
	UNION
	SELECT * FROM Associations
	WHERE word1 IN (SELECT word2 FROM Associations WHERE word1=w AND grade>=n)
	AND word2 IN (SELECT word2 FROM Associations WHERE word1=w AND grade>=n)) t
	UNION
	SELECT ']}';
END//

DELIMITER ;

CALL term_network('address', 2);

DROP PROCEDURE IF EXISTS term_network_nodes;

DELIMITER //

CREATE PROCEDURE term_network_nodes(IN w VARCHAR(100), n DOUBLE)
BEGIN
	SELECT LOWER(word2)
    FROM Associations 
    WHERE word1=w AND grade>=n 
    UNION SELECT w;	
END//

DELIMITER ;

CALL term_network_nodes('address', 2);



DROP PROCEDURE IF EXISTS term_network_links;

DELIMITER //

CREATE PROCEDURE term_network_links(IN w VARCHAR(100), n DOUBLE)
BEGIN

	SELECT LOWER(word1) AS Source, LOWER(word2) AS Target, grade/2 AS Grade 
    FROM (
	SELECT * 
    FROM Associations 
    WHERE word1 = w AND grade>=n
	UNION
	SELECT * 
    FROM Associations
	WHERE word1 IN (SELECT word2 FROM Associations WHERE word1=w AND grade>=n)
	AND word2 IN (SELECT word2 FROM Associations WHERE word1=w AND grade>=n)) t;
	
END//

DELIMITER ;

CALL term_network_links('address', 2);