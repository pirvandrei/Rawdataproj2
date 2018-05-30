
define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        var self = this;
        var questionData = ko.observableArray();
        var answerData = ko.observableArray();
        var qTitle = ko.observable();
        var qBody = ko.observable();
        var qScore = ko.observable();
        var qComments = ko.observable();
        var qTags = ko.observable();
        var acceptedAnswer = ko.observable()
        var qNotes = ko.observableArray(null);
        var qUserName = ko.observable();
        var qCreationDate = ko.observable();
        var countAnswers = ko.observable();
        var countNotes = ko.observable();
        var qBookmarked = ko.observable();
        var id = ko.observable();
        var notes = ko.observableArray();
        var noteText = ko.observable();
        var showAddNote = ko.observable(true);
        //First we need to load question data

        var addBookmark = function () {
            req.saveBookmark({ postid: id(), userid: 1 }, function (data) {
                qBookmarked(true);
            }); 
        }

        var removeBookmark = function () {
            req.deleteBookmark({ postid: id() }, function (data) {
                qBookmarked(false);
            });
        }

        var addNote = function () {
            req.saveNote({ text: noteText(), postid: id(), userid: 1 }, function (data) {
                console.log(data);
                req.getQuestion(params, function (data) {
                    notes(data.notes);
                    showAddNote(false);
                });
            });
        }

        var removeNote = function () {
            req.deleteNote({ postid: id() }, function (data) {
                console.log(data);
                req.getQuestion(params, function (data) {                  
                    notes(data.notes);
                    countNotes(data.notes.length);
                    showAddNote(true);                       
                });
            });
            
        }
        

        //load question data
    ko.computed(function () {
        req.getQuestion(params, function (data) {
            console.log(data)
            answerData(data.answers);
            notes(data.notes);
            console.log(answerData());
            qTitle(data.title);
            qBody(data.body);
            qScore(data.score);
            qComments(data.comments);
            qTags(data.postTags);
            qNotes(data.notes);
            qUserName(data.userName);
            acceptedAnswer(data.acceptedAnswerID);
            qCreationDate(data.creationdate);
            countAnswers(data.comments.length);     
            qBookmarked(data.bookmarked);
            id(data.id);
            countNotes(data.notes.length);
            });
        });
    return {
        questionData,
        answerData,
        qTitle,
        qBody,
        qScore,
        qComments,
        qTags,
        qNotes,
        qUserName,
        qCreationDate,
        countAnswers,
        acceptedAnswer,
        qBookmarked,
        addBookmark,
        removeBookmark,
        addNote,
        removeNote,
        notes,
        noteText,
        countNotes,
        showAddNote
        };
    };
});
