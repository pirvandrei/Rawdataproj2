define(['knockout', 'request'], function (ko, req) {
    return function (params) {
        var searchString = ko.observable(params.searchString);
        var searchMethod = ko.observable(params.searchMethod);
        var startDate = ko.observable(params.startDate);
        var endDate = ko.observable(params.endDate);
        var currentPage = ko.observableArray([1,2,3,4,5]);
        var pData = ko.observable();
        //Page object that we will user for saving already loaded pages
        var SavedPage = function (pageNr, resultList) {
            this.pageNr = pageNr
            this.resultList = resultList
        }

        //
        var PageData = function (data) {
            this.prev = data.prev
            this.next = data.next
            this.total = data.total;
            this.pages = data.pages;
            this.posts = data.posts;
            this.savedPages = ko.observableArray();
            this.location = ko.observable(1);
            this.update = function (direction) {
                //do if we go forward

            }
        }
        var changePage= function (direction) {
            if (direction === 'next') {
             //do if we go backward

            } else {
            }
        }


        ko.computed(function () {
            var data = { searchString: searchString(), searchMethod: searchMethod() };
            //make api call
            req.getSearchResults(params, function (data) {
                //Initialize and save object used for current page tracking
                pData(new PageData(data));
                currentPage(pData().posts)
            });
        });

        

    return {
        currentPage,
        changePage,
        pData
        };
    };
});
