DROP INDEX wi_word_index ON wi;
CREATE INDEX wi_word_index ON wi(word);


DROP INDEX tfidf_term_index ON tfidf;
CREATE INDEX tfidf_term_index ON tfidf(term);