define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        // public partv
        var score = ko.observable(params.score);
        var userName = ko.observable(params.userName);
        var notes = ko.observable(params.notes);
        var body = ko.observable(params.body);
        var score = ko.observable(params.score);
        var comments = ko.observableArray(params.comments);
        var id = ko.observable(params.id);
        var answerID = ko.observable(params.answerID());
        var bookedmarked = ko.observable(params.bookmarked);

        var addBookmark = function () {
            req.saveBookmark({ postid: id(), userid: 1 }, function (data) {       
                bookedmarked(true);               
            });
        }

        var removeBookmark = function () {
            req.deleteBookmark({ postid: id() }, function (data) {
                bookedmarked(false);
            });
        }

        return {
            score,
            userName,
            notes,
            body,
            score,
            comments,
            answerID,
            id,
            bookedmarked,
            addBookmark,
            removeBookmark
        };
    };
});
