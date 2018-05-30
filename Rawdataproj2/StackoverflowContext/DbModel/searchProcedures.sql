DROP PROCEDURE IF EXISTS MatchAll_Count;

DELIMITER //

CREATE PROCEDURE MatchAll_Count(param VARCHAR(1000), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN
	SET @p = LENGTH(param) - LENGTH(REPLACE(param, ' ', '')) + 1;
	SET @s = 'SELECT COUNT(*) FROM (SELECT p.id, p.title, p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, SUM(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from words where word = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.id AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @s = CONCAT(@s,' GROUP BY p.id, p.Body HAVING rank =', @p, ') q'); 
	PREPARE stmt FROM @s;
	EXECUTE stmt;
END //
DELIMITER ;

CALL MatchAll_Count('"sql" "html" "java"', "'2010-01-01'", "'2018-05-17'");		

----------------------------------------------------------------------------------------------------------------------------

DROP PROCEDURE IF EXISTS BestMatchRanked_Count;

DELIMITER //

CREATE PROCEDURE BestMatchRanked_Count(param VARCHAR(1000), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN
	SET @s = 'SELECT COUNT(*) FROM (SELECT p.id, p.title, p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, SUM(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from wi where word = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.id AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @s = CONCAT(@s,' GROUP BY p.id, p.Body ORDER BY rank DESC) q'); 
    PREPARE stmt FROM @s;
	EXECUTE stmt;
END //

DELIMITER ;

CALL BestMatchRanked_Count('"SQL"', "'2010-01-01'", "'2018-05-28'");


----------------------------------------------------------------------------------------------------------------------------

DROP PROCEDURE IF EXISTS BestMatchWeighted_Count;

DELIMITER //

CREATE PROCEDURE BestMatchWeighted_Count(param VARCHAR(1000), startDate VARCHAR(1000), endDate VARCHAR(1000))
BEGIN	

	SET @s = 'SELECT COUNT(*) FROM (SELECT p.id, p.title, p.Body, p.PostType, p.CreationDate, p.AcceptedAnswerID, SUM(t.rdt) rank FROM Posts p, (SELECT distinct document, rdt from tfidf where term = ';
	SET @s = CONCAT(@s,REPLACE(param, ' ', ' UNION ALL SELECT distinct document, rdt from tfidf where term ='));
	SET @s = CONCAT(@s,') t WHERE p.id = t.document AND CreationDate BETWEEN ', startDate, ' AND ', endDate);
    SET @S = CONCAT(@s, ' GROUP BY p.id, p.Body ORDER BY rank DESC) q');  	
    PREPARE stmt FROM @s;
	EXECUTE stmt;
END //

DELIMITER ;

CALL BestMatchWeighted_Count('"sql" "html" "java"', "'2014-01-01'", "'2018-05-17'");

