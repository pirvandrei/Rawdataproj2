define(['knockout', 'request','paginate'], function (ko, req,pg) {
    return function (params) {
        var searchString = ko.observable(params.searchString);
        var searchMethod = ko.observable(params.searchMethod);
        var startDate = ko.observable(params.startDate);
        var endDate = ko.observable(params.endDate);
        var currentPage = ko.observableArray([1,2,3,4,5]);
        var pData = ko.observable();
        var changePage = ko.observable();
        var currentPagenr = ko.observable();
        var previousLink = ko.observable;
        var nextLink = ko.observable;
        var cPage = ko.observableArray();
    
        var changePageNext = function () {
            if (pg.moveCursor('next')) {
                cPage(pg.data.items)
                console.log(pg.data.items)
            }
        }
        var changePagePrev = function () {
            if (pg.moveCursor('prev')) {
                cPage(pg.data.items)
                console.log(pg.data.items)

            }
        }
        ko.computed(function () {
            var data = { searchString: searchString(), searchMethod: searchMethod() };
            //make api call
            req.getSearchResults(params, function (data) {
                pg.loadData(data)
                cPage(pg.data.items)
                currentPagenr(pg.data.currentPage);
                console.log(currentPagenr());
            });
        });

        

    return {
        currentPage,
        changePage,
        cPage,
        pg,
        currentPagenr,
        changePageNext,
        changePagePrev
        };
    };
});
