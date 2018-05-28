define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        console.log(params)
        var date = ko.observable(params.data.date);
        var text = ko.observable(params.data.text);
        //redirect to target
        var goToSearch = function () {
            window.location = '/#/search?searchString=' + text();
        }
        return {
            date,
            text,
            goToSearch
        };
    };
});
