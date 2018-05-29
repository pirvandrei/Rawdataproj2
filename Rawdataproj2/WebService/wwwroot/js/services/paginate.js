define(['request'], function (req) {
    var self = this;
    this.data = {}
    this.loadData = function (data) {
        self.data.next = data.next;
        self.data.prev = data.prev;
        self.data.total = data.total;
        self.data.pages = data.pages;
        self.data.items = data.items;
        self.data.target = data.target;
        self.data.currentPage = data.currentpage;
        console.log(self.data);
    }
    this.moveCursor = function (direction) {
        //Load next/previous page 
        if (direction === 'next') {
           if (self.data.currentPage === self.data.total) {
                return false;
            } else {
               if (self.loadPage(self.data.next));
                return true;
            }
        } else if (direction === 'prev') {
            if (self.data.currentPage > 1) {
                self.loadPage(self.data.prev);
                return true;
            } else {
                return false;
            }
        }
    }
    this.loadPage = function (target) {
        req.getPage({ target: target }, function (data) {
            self.loadData(data);
            return true;
        });
    }

    return {
        loadData,
        data,
        loadPage,
        moveCursor
    }
})