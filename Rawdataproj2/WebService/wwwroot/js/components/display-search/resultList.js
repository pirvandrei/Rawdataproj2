
define(['knockout', 'request','paginate'], function (ko, req,pg) {
    return function (params) {
       
        var searchString = ko.observable(params.searchString);
        var searchMethod = ko.observable(params.selectedMethod);
        var startDate = ko.observable(params.startDate);
        var endDate = ko.observable(params.endDate);

        var currentPagenr = ko.observable(1);
        var previousLink = ko.observable;
        var nextLink = ko.observable;
        var cPage = ko.observableArray();
        var noData = ko.observable();
        var total = ko.observable();
        var pages = ko.observable();
        
        //Pagination button bindings
        var changePageNext = function () {
            if (pg.moveCursor('next')) {
                setTimeout(function () {
                    cPage(pg.data.items)
                    currentPagenr(pg.data.currentPage);
                    window.scrollTo(0, 0);
                },500);
            }
        }

        var changePagePrev = function () {
            if (pg.moveCursor('prev')) {
                    setTimeout(function () {
                    cPage(pg.data.items)
                    currentPagenr(pg.data.currentPage);
                    window.scrollTo(0,0);
                }, 500);
            }
        }
        ko.computed(function () {
            var data = { searchString: searchString(), searchMethod:searchMethod() };
            //make api call
            req.getSearchResults(data, function (data) {
                pg.loadData(data);
                cPage(pg.data.items);
                currentPagenr(pg.data.currentPage);
                total(data.total);
                pages(data.pages);
                if (pg.data.pages == null) {
                    noData(true)
                } else {
                    noData(false)
                }
                
            });
        });        

    return {
        cPage,
        pg,
        currentPagenr,
        changePageNext,
        changePagePrev,
        noData,
        total,
        pages
        };
    };
});
