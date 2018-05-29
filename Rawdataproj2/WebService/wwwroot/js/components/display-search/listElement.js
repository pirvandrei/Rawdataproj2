define(['knockout'], function (ko, param) {
    return function (params) {
        // public part
        var title = ko.observable(params.title);
        var score = ko.observable(params.score);
        var userName = ko.observable(params.userName);
        var body = ko.observable(params.body);
        var parentId = ko.observable(params.parentId);
        var creationDate = ko.computed(function () {
            return 'Asked on: ' + params.creationDate
        });
        var id = ko.observable(params.id);
        var goToPost = function () {
            //Check if post has parent id if yes then use that parent id as id instead
            var temp = null;
            console.log(parentId);
            if (parentId() != null) {
                temp = parentId();
            } else {
                temp = id();
            }
            window.location = '#/post/'+temp;
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
