define([], function () {
    var Paginate = function (data){
        var next = data.next;
        var prev = data.prev;
        var total = data.total;
        var pages = data.pages;
        var items = data.items;
        var currentPage = 1;
        var pageHistory = [];
        var moveCursor = function (direction){
            //First move page
            if (direction === 'next') {
                currentPage++;

            }else if(direction ==='prev'){
                currentPage--;
            }
            
            
        }
        var loadPage = function () {
            //save page to history object
        }
        var savePage = function () {
        } 
    }

    return {
    }
})