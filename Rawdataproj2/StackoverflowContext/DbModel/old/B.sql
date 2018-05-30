-- B1 - select multiple based on inverted index 
use raw2;
drop table if exists wi; 
create table wi as select id, word from words  
where word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' 
and tablename = 'posts' 
and (what='title' or what='body') 
group by id,word;


 
-- search all words given in the query, atm it doesn't take 
-- records that contain all the words, but these that contain at least 1
drop procedure if exists bestmatch;
delimiter //
CREATE PROCEDURE bestmatch(param VARCHAR(1000))
BEGIN
SET @s = 'SELECT p.id FROM Posts p, (SELECT distinct id from words where word = ';
SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct id from wi where word ='));
SET @s = concat(@s,') t WHERE p.id = t.id GROUP BY p.id;');
-- SELECT @s;
PREPARE stmt FROM @s;
EXECUTE stmt;
end //
Delimiter ;
CALL bestmatch('"sql", "html", "java"');


select * from Posts where id =1672;
 
-- relevant words
SELECT word, count(word) as score FROM raw2.words where id = 19 and word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' group by word order by score desc;
 SELECT * FROM raw2.words where id = 19;
 
 
 