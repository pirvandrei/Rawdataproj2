﻿
define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        var test = ko.observable('sdfsd');
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
        //First we need to load question data

    ko.computed(function () {
        req.getQuestion(params, function (data) {
            console.log(data)
            answerData(data.answers);
            console.log(answerData());
            qTitle(data.title);
            qBody(data.body);
            qScore(data.score);
            qComments(data.comments);
            qTags(data.postTags);
            qNotes(data.notes);
            qUserName(data.UserName);
            acceptedAnswer(data.acceptedAnswerID);

            
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
        qUserName
        };
    };
});
