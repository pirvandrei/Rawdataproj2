define(['knockout','request','paginate'], function (ko, req,pg) {
    return function (params) {
        var notes = ko.observableArray();
        var id = 1;
        var currentPagenr = ko.observable();
        var previousLink = ko.observable;
        var nextLink = ko.observable;
        var cPage = ko.observableArray();
        var total = ko.observable();
        var pages = ko.observable();

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
        ko.computed(function () {
            //make api call
            req.getNotes({ id: id }, function (data) {
                pg.loadData(data)
                cPage(pg.data.items)
                currentPagenr(pg.data.currentPage);
                console.log(cPage());
                total(data.total);
                pages(data.pages);
            });
        });

        return {
            cPage,
            pg,
            currentPagenr,
            changePageNext,
            changePagePrev,
            total,
            pages
        };
    };
});
