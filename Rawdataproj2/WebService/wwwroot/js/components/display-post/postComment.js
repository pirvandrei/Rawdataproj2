define(['knockout'], function (ko) {
    return function (params) {
        // public part
        var score = ko.observable(params.score);
        var text = ko.observable(params.text);
        var userName = ko.observable(params.userName);
        var creationDate = ko.observable(params.creationDate);

        return {
            score,
            text,
            userName,
            creationDate
        };
    };
});
