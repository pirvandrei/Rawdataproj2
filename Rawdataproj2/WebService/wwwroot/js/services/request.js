define([], function () {

    var getQuestion = function (data,callback) {
        fetch("api/questions/" + data.id)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getSearchHistory = function (data, callback) {
        fetch("api/history/" + data.id)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getSearchResults = function (data, callback) {
        fetch("api/search?query=" + data.searchString + "&method=" + data.searchMethod)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getPage = function (data, callback) {
        fetch(data.pageAddress)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };


    return {
        getQuestion,
        getSearchHistory,
        getSearchResults,
        getPage
    }


});