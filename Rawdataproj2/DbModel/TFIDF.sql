USE raw2;   
select * from words;

-- n(t)
SET @nt = (SELECT count(distinct id) FROM raw2.words where word = 'value'
and tablename = 'posts'
and (what = 'title' or what = 'body')) ; 
Select @nt;
-- add column score to words
ALTER TABLE words 
ADD nt int(11); 

ALTER TABLE words 
drop nt; 

drop procedure if exists fillNt; 
delimiter // 
create procedure fillNt (in pWord varchar(100)) 
begin 
update words set nt = @nt where word = pWord; 
end 
// delimiter ; 
CALL fillNt('value');

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

-- r(d,t) = TF(d,t) * IDF(t)
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
