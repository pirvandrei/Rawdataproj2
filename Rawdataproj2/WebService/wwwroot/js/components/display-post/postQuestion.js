define(['knockout'], function (ko) {
    return function (params) {
        // public partv
        var test = ko.observable(params.test());
        var title = ko.observable();
        var body = ko.observable();
        var score = ko.observable();
        var comments = ko.observable({});
        setTimeout(
            function () {
                console.log(params.post())
                title(params.post().title);
                body(params.post().body);
                score(params.post().score)
                comments(params.post().comments)
            },2000
        )

        return {
            title,
            body,
            score,test,comments
        };
    };
});
