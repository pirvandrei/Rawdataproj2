-- QUESTION 11; sss

use raw2;
SET SQL_SAFE_UPDATES = 0;

-- relevant words
SELECT word, count(word) as score FROM raw2.words where id = 19 and word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' group by word order by score desc;

-- PART A 
-- add column score to words
ALTER TABLE words 
ADD score int(11);  

-- update score
UPDATE words SET score = 1
WHERE word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$'
and tablename = 'posts' 
and (what='title' or what='body');
  
-- PART B
use raw2;
drop table if exists wi; 
create table wi as select id, word from words  
where word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' 
and tablename = 'posts' 
and (what='title' or what='body') 
group by id,word;

-- PART B
use raw2;
drop table if exists wi; 
create table wi as select id, word from words  
where word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' 
and tablename = 'posts' 
and (what='title' or what='body') 
group by id,word;

drop procedure if exists bestmatch3; 
delimiter // 
create procedure bestmatch3 (in w1 varchar(100),in w2 varchar(100),in w3 varchar(100)) 
begin 
select Posts.id, sum(t.score) rank, body from Posts,
	(select distinct id,wi.score from wi where word = w1 
	union all 
	select distinct id,wi.score from wi where word = w2 
	union all 
	select distinct id,wi.score from wi where word = w3) t 
where t.id=Posts.id group by t.id order by rank desc; 
end 
// delimiter ; 
CALL bestmatch3('sql','html','java');


-- drop procedure if exists find;
-- delimiter //
-- create procedure find(w1 varchar(100), w2 varchar(100))
-- begin
-- SET @s="select body from posts where postid in (select id from words where word = '";
-- SET @s=concat(@s,w1);
-- SET @s=concat(@s,"') and postid in (select id from words where word = '");
-- SET @s=concat(@s,w2);
-- SET @s=concat(@s,"');");
-- PREPARE stmt FROM @s;
-- EXECUTE stmt;
-- end //
-- delimiter ;
-- CALL find('sql','injection');



GRANT EXECUTE ON raw2.* TO 'aip';


