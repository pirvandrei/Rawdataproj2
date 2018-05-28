define(['knockout','request'], function (ko,req) {
    return function (params) {
        // public part
        //load history data
        var id = 1;
        var searchString = ko.observable();
        var historyItems = ko.observableArray();

        ko.computed(function () {
            req.getSearchHistory({ id: id }, function (data) {
                historyItems(data.searchhistory);
                console.log(historyItems())
            });
        });
        return {
            historyItems
        };
    };
});
