define(['knockout','request','paginate'], function (ko,req,pg) {
    return function (params) {
        // public part
        //load history data
        var id = 1;
        var searchString = ko.observable();
        var historyItems = ko.observableArray();
        var currentPagenr = ko.observable();
        var previousLink = ko.observable;
        var nextLink = ko.observable;
        var cPage = ko.observableArray();

        //Pagination button bindings
        var changePageNext = function () {
            if (pg.moveCursor('next')) {

                setTimeout(function () {
                    cPage(pg.data.items)
                    currentPagenr(pg.data.currentPage);
                    window.scrollTo(0, 0);
                }, 500);
            }
        }

        var changePagePrev = function () {
            if (pg.moveCursor('prev')) {
                setTimeout(function () {
                    cPage(pg.data.items)
                    currentPagenr(pg.data.currentPage);
                    window.scrollTo(0, 0);
                }, 500);
            }
        }
        //Initial load
        ko.computed(function () {
            req.getSearchHistory({ id: id }, function (data) {
                console.log(data)
                pg.loadData(data)
                cPage(pg.data.items)
                currentPagenr(pg.data.currentPage);
            });
        });
        return {
            currentPagenr,
            changePageNext,
            changePagePrev,
            cPage
        };
    };
});
