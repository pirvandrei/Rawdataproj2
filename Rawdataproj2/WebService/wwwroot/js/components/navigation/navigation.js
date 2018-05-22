define(['knockout'], function (ko) {
    return function (params) {
        var searchString = ko.observable("searchString")

        // public part
        return {
            searchString
        };
    };
});