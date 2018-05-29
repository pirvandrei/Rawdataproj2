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
                console.log(response)
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
        fetch(data.target)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getNotes = function (data, callback) {
        fetch('api/notes?userid='+data.id)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    //POST requests
    var saveSearch = function (data, callback) {
        fetch('api/history', {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var saveBookmark = function (data, callback) {
        fetch('api/booksmarks'+data.userid, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var deleteBookmark = function (data, callback) {
        fetch('api/booksmarks' + data.userid, {
            method: 'DELETE',
            body: JSON.stringify(data),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };

    return {
        getQuestion,
        getSearchHistory,
        getSearchResults,
        getPage,
        saveSearch,
        getNotes
    }


});