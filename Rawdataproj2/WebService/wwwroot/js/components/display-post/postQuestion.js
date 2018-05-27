define(['knockout'], function (ko) {
    return function (params) {
        // public partv
        console.log(params)
        var score = ko.observable(params.score);
        var userName = ko.observable(params.userName);
        var notes = ko.observable(params.notes);
        var body = ko.observable(params.body);
        var score = ko.observable(params.score);
        var comments = ko.observable(params.comments);
        var title = ko.observable(params.title);
        var tags = ko.observable();
        console.log(ko.unwrap(params.comments()))


        return {
            title,
            body,
            score,
            comments,tags
        };
    };
});
