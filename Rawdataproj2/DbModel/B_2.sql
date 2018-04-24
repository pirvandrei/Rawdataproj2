USE raw2;
-- B1
DROP PROCEDURE IF EXISTS bestmatch;
DELIMITER //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN
    SET @s = 'SELECT p.id FROM Posts p, (SELECT distinct id from wi where word = ';
    SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct id from wi where word ='));
    SET @s = concat(@s,') t WHERE p.id = t.id GROUP BY p.id;');
    
    PREPARE stmt FROM @s;
    EXECUTE stmt;
END //
DELIMITER ;
CALL bestmatch('"sql", "html", "java"');
-- B2
DROP PROCEDURE IF EXISTS bestmatch;
DELIMITER //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN   
    SET @s = 'SELECT p.id, SUM(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from wi where word = ';
    SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
    SET @s = concat(@s,') t WHERE p.id = t.id GROUP BY p.id ORDER BY rank DESC');  
    
    PREPARE stmt FROM @s;
    EXECUTE stmt;
END //
DELIMITER ;
CALL bestmatch('"sql", "html", "java"');
CALL bestmatch('"Hanselman"');
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
    ADD tf  double(2,2),
    ADD idf decimal(32,20), 
    ADD rdt double(2,2);
    
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
DROP PROCEDURE IF EXISTS bestmatch;
DELIMITER //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN   
    SET @s = 'SELECT p.id, SUM(t.rdt) rank FROM Posts p, (SELECT distinct document, rdt from tfidf where term = ';
    SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct document, rdt from tfidf where term ='));
    SET @s = concat(@s,') t WHERE p.id = t.document GROUP BY p.id ORDER BY rank DESC');  
    
    PREPARE stmt FROM @s;
    EXECUTE stmt;
END //
DELIMITER ;
CALL bestmatch('"sql", "html", "java"');
-- B5
DROP PROCEDURE IF EXISTS bestmatch;
DELIMITER //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN   
    SET @s = 'SELECT word, SUM(rank) rank FROM wi, (SELECT id, SUM(t.score) rank FROM (SELECT distinct id, 1 score from wi where word = ';
    SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
    SET @s = concat(@s,') t GROUP BY id) t1 WHERE wi.id = t1.id GROUP BY word ORDER BY rank DESC');  
    
    PREPARE stmt FROM @s;
    EXECUTE stmt;
END //
DELIMITER ;
CALL bestmatch('"sql", "html", "java"');
-- B6
DROP PROCEDURE IF EXISTS bestmatch;
DELIMITER //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN   
    SET @s = 'SELECT term, SUM(rank) rank FROM tfidf, (SELECT document, SUM(t.rdt) rank FROM (SELECT distinct document, rdt from tfidf where term = ';
    SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct document, rdt from tfidf where term ='));
    SET @s = concat(@s,') t GROUP BY document) t1 WHERE tfidf.document = t1.document GROUP BY term ORDER BY rank DESC');  
    
    PREPARE stmt FROM @s;
    EXECUTE stmt;
END //
DELIMITER ;
CALL bestmatch('"sql", "html", "java"');
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
DROP PROCEDURE IF EXISTS getAssociations;
DELIMITER //
CREATE PROCEDURE getAssociations(param VARCHAR(1000))
BEGIN   
    SELECT word2, grade
    FROM Associations
    WHERE word1 = param
    ORDER BY grade DESC;    
END //
DELIMITER ;
CALL getAssociations('DataTable');
-- B9
drop procedure if exists term_network;
delimiter //
create procedure term_network(in w varchar(100), n double)
begin
select 'var graph = '
union
select '{"nodes":['
union
select ''
union
select concat('{"id":"',lower(word2),'"},') from Associations where word1=w and grade>=n union select concat('{"id":"',w,'"},')
union
select '],'
union
select '"links":['
union
select concat('{"source":"',lower(word1),'", "target":"', lower(word2),'", "value":',grade/2,'},') line from (
select * from Associations where word1 = w and grade>=n
union
select * from Associations
where word1 in (select word2 from Associations where word1=w and grade>=n)
and word2 in (select word2 from Associations where word1=w and grade>=n)) t
union
select ']}';
end//
delimiter ;
call term_network('address', 2);