use raw2;
SET SQL_SAFE_UPDATES = 0;

DROP PROCEDURE IF EXISTS getposts;
DELIMITER //
CREATE PROCEDURE getposts(s varchar(100)) 
BEGIN
		select * from Posts
		where body like concat('%', s, '%');
END //
DELIMITER ;
CALL getposts('Hanselman');

select * from Posts where body like '%Hanselman%';
        
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
CALL bestmatch('"Hanselman"');

drop table if exists temp1;
create table temp1 ( id int(11), rank int (11));
  -- helpers
  drop procedure if exists bestmatch3; 
delimiter // 
create procedure bestmatch3 (in w1 varchar(100)) 
begin 
select Posts.id, sum(t.score) rank, body from Posts,
	(select distinct id,wi.score from wi where word = w1 
	union all 
	select distinct id,wi.score from wi where word = w2 
	union all 
	select distinct id,wi.score from wi where word = w3) t 
where t.id=Posts.id group by t.id order by rank desc into temp1 ; 
end 
// delimiter ; 
CALL bestmatch3('Hanselman');


-- 19/4 - TFIDF - Importance based on relevance 
 

ALTER TABLE Posts
CHANGE COLUMN 
`post_type` `PostType` int(11);

ALTER TABLE Posts 
ADD post_type varchar(1000) DEFAULT 'N';

ALTER TABLE words 
ADD score int(11);

 
ALTER TABLE Posts 
ADD Title mediumtext,
ADD AcceptedAnswerID int(11),
ADD ClosedDate datetime,
ADD ParentID int(11);

 
select * from raw2.Posts; 
select id, post_type from raw2.Posts; 
SELECT id, posttypeid FROM stackoverflow_sample_universal.posts;
 
UPDATE raw2.Posts 
INNER JOIN stackoverflow_sample_universal.posts ON raw2.Posts.id = stackoverflow_sample_universal.posts.id 
SET raw2.Posts.post_type = stackoverflow_sample_universal.posts.posttypeid; 


select PostID,Title, AcceptedanswerID, Closeddate from raw2.Questions; 
select PostID, ParentID from raw2.Answers;
select ID, ParentID from raw2.Posts;
select ID, Title, AcceptedAnswerID, ClosedDate from raw2.Posts;
select ID, ClosedDate from raw2.Posts;


select count(distinct ID) from raw2.Posts
	 where AcceptedAnswerID is not null;
     
select count(distinct ID) from stackoverflow_sample_universal.posts
	 where AcceptedAnswerID is not null;

-- populate question related attributes
USE raw2;
UPDATE Posts INNER JOIN Questions ON Posts.ID = Questions.PostID 
	SET Posts.Title = Questions.Title,
	 Posts.AcceptedAnswerID = Questions.AcceptedanswerID,
     Posts.ClosedDate = Questions.Closeddate;
   
-- populate answer related attributes-- 
UPDATE Posts INNER JOIN Answers ON Posts.ID = Answers.PostID SET Posts.ParentID = Answers.ParentID;


select count(distinct ID) from raw2.Posts
	 where AcceptedAnswerID is not null;
     
select count(distinct ID) from stackoverflow_sample_universal.posts
	 where AcceptedAnswerID is not null;
     
select count(distinct ID) from raw2.Posts
	 where Title is not null and ClosedDate is not null;
     
select count(distinct ID) from stackoverflow_sample_universal.posts
	 where Title is not null and Closeddate is not null ;
     
     
 use raw2;
 Drop table Questions;
 Drop table Answers;
-- UPDATE table1 INNER JOIN table2 ON table1.id = table2.id SET table1.Price = table2.price
-- 

-- UPDATE `raw2`.`Posts`
-- SET
-- `post_type` = <{post_type}>
-- WHERE <{where_expression}>;

