use raw2;
ALTER TABLE Posts 
ADD post_type varchar(1000) DEFAULT 'N';
  
ALTER TABLE Posts
CHANGE COLUMN 
`post_type` `PostType` int(11);

select id, post_type from raw2.Posts;
 
 
SELECT id, posttypeid FROM stackoverflow_sample_universal.posts;

 
SET SQL_SAFE_UPDATES = 0;





UPDATE raw2.Posts 
INNER JOIN stackoverflow_sample_universal.posts ON raw2.Posts.id = stackoverflow_sample_universal.posts.id 
SET raw2.Posts.post_type = stackoverflow_sample_universal.posts.posttypeid 



-- UPDATE `raw2`.`Posts`
-- SET
-- `post_type` = <{post_type}>
-- WHERE <{where_expression}>;