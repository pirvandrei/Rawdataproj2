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
    var searchResults = function (data, callback) {
        fetch("pi/search?query="+data.query)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };


    return {
        getQuestion,
        getSearchHistory,
        searchResults
    }


});