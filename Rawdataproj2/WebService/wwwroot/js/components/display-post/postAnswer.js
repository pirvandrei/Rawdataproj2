define(['knockout'], function (ko, param) {
    return function (params) {
        // public partv
        var score = ko.observable(params.score);
        var userName = ko.observable(params.userName);
        var notes = ko.observable(params.notes);
        var body = ko.observable(params.body);
        var score = ko.observable(params.score);
        var comments = ko.observableArray(params.comments);
        return {
            score,
            userName,
            notes,
            body,
            score,
            comments
        };
    };
});
