USE raw2;   
select * from words;
SET SQL_SAFE_UPDATES = 0;





drop procedure if exists tfidf;
delimiter //
CREATE PROCEDURE tfidf(param VARCHAR(1000))
BEGIN
SET @s = 'SELECT p.id, sum(t.score) rank FROM Posts p, (SELECT distinct id, 1 score from words where word = ';
SET @s = concat(@s,replace(param, ',', ' UNION ALL SELECT distinct id, 1 score from wi where word ='));
SET @s = concat(@s,') t WHERE p.id = t.id GROUP BY p.id ORDER BY rank desc;');
-- SELECT @s;
PREPARE stmt FROM @s;
EXECUTE stmt;
end //
Delimiter ;
CALL bestmatch('"sql", "html", "java"'); 

-- Importance based on relevance 

-- create tfidf with document and words
use raw2;
drop table if exists tfidf_1; 
create table tfidf_1 as select id as document, word as term, count(word) as nt from words  
where word regexp '^[A-Za-z][A-Za-z0-9_]{1,}$' 
and tablename = 'posts' 
and (what='title' or what='body');

select * from tfidf_1;
-- alter tfidf with n(t), n(d,t), n(d), TF(d,t), IDF(t), r(d,t)
ALTER TABLE tfidf_1 
	ADD ndt int(11), 
	ADD nd int(11),
	ADD tf  double(2,2),
	ADD idf double(2,2), 
    ADD rdt double(2,2);

-- create table n(d)
drop table if exists nd; 
create table nd as (select document, count(term) as nd from tfidf_1 group by document);
 
-- test 
select id, word from wi where id = 12210807;
select * from nd; 



-- create table n(d,t)
drop table if exists ndt;
create table ndt as (select distinct document, term, count(term) as ndt from tfidf_1 group by term, document);

-- test 
select * from ndt; 
select document, count(term) from tfidf_1 as t1, (select document from tfidf_1 group by document) t 
	where t1.document = t.documnt
    group by term; 
select distinct id, count(word) from words ;



-- CREATE INDEX idx_word ON words (word);
-- CREATE INDEX idx_tfidx ON tfidf_1 (rdt);


Update tfidf_1
	set nd = (select term from tfidf_1 group by document);

  
SET @nt = (SELECT count(distinct id) FROM raw2.words where word = 'value'
and tablename = 'posts'
and (what = 'title' or what = 'body')) ; 
 

Select distinct document, term, nt, ndt, nd, tf, idf, rdt from tfidf_1;



-- Relevance weighted word list  
   
 



-- weighted word list 
-- step1
select id, sum(score) rank from
(select distinct id, 1 score from wi where word = 'using'
union all
select distinct id, 1 score from wi where word = 'regions'
union all
select distinct id, 1 score from wi where word = 'blocks') t1
group by id;

-- step2 
select wi.id, word, rank from wi,
(select id, sum(score) rank from
(select distinct id, 1 score from wi where word = 'using'
union all
select distinct id, 1 score from wi where word = 'regions'
union all
select distinct id, 1 score from wi where word = 'blocks') t1
group by id) t2
where wi.id=t2.id;

-- step3 
-- select wi.id, word, rank from wi,
select word,sum(rank) srank from wi,
(select id, sum(score) rank from
(select distinct id, 1 score from wi where word = 'using'
union all
select distinct id, 1 score from wi where word = 'regions'
union all
select distinct id, 1 score from wi where word = 'blocks') t1
group by id ) t2
where wi.id=t2.id 
group by word order by srank desc limit 10;



















-- helpers 
-- n(t)
SET @nt = (SELECT count(distinct id) FROM raw2.words where word = 'value'
and tablename = 'posts'
and (what = 'title' or what = 'body')) ; 
Select @nt; 
-- n(d,t)
SET @ndt = (SELECT count(*) FROM raw2.words where word = 'value' and id = 19
and tablename = 'posts'
and (what = 'title' or what = 'body'));
Select @ndt; 
-- n(d)
SET @nd =  (SELECT count(distinct word) FROM raw2.words where id = 19
and tablename = 'posts'
and (what = 'title' or what = 'body'));
Select @nd; 
-- TF(d,t) = log(1+n(d,t)/n(d))
SET @tf = (LOG(2,(1+ @ndt/@nd)));
Select @tf;   
-- IDF(t) = 1 / n(t)
SET @idf = (1/@nt);
Select @idf;   
-- r(d,t) = TF(d,t) * IDF(t); -- document, word, n(t), n(d,t), n(d), TF(d,t), IDF(t)
SET @rdt = @tf*@idf;
Select @rdt;











-- TF(d,t) = log(1+n(d,t)/n(d)) 
-- SET @nt = (SELECT LOG(2, (1 + ((SELECT count(*) FROM raw2.words where word = 'value' and id = 19
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body'))/(
-- SELECT count(distinct word) FROM raw2.words where id = 19
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body'))))));

 
-- IDF(t) = 1 / n(t) 
-- SET @idf = (SELECT 1/(SELECT count(distinct id) FROM raw2.words where word = 'value'
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body')));
-- 


-- r(d,t) = TF(d,t) * IDF(t) 
-- SELECT (SELECT LOG(2, (1 + ((SELECT count(*) FROM raw2.words where word = 'value' and id = 19
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body'))/(
-- SELECT count(distinct word) FROM raw2.words where id = 19
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body')))))) * (SELECT 1/(SELECT count(distinct id) FROM raw2.words where word = 'value'
-- and tablename = 'posts'
-- and (what = 'title' or what = 'body')) as IDF); 
