define(['knockout'], function (ko, param) {
    return function (params) {
        // public part
        var title = ko.observable(params.title);
        var score = ko.observable(params.score);
        var userName = ko.observable(params.userName);
        var body = ko.observable(params.body);
        var creationDate = ko.computed(function () {
            return 'Asked on: ' + params.creationDate
        });
        var id = ko.observable(params.id);
        var goToPost = function () {
            window.location = '#/post/'+id();

        }
        return {
            id,
            title,
            score,
            creationDate,
            userName,
            body,
            goToPost
        };
    };
});
