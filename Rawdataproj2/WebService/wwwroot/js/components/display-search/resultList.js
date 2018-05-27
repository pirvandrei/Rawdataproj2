define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        var searchString = ko.observable(params.searchString);
        var searchMethod = ko.observable(params.searchMethod);
        var startDate = ko.observable(params.startDate);
        var endDate = ko.observable(params.endDate);
        var resultList = ko.observable();
        var self = this;

        ko.computed(function () {
            var data = { searchString: searchString(), searchMethod: searchMethod() };
            //make api call
            req.getSearchResults(params, function (data) {
                console.log(data);
            });
        });

        

    return {
            resultList
        };
    };
});
