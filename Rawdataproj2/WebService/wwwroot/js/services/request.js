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
        fetch('api/search?query=' + data.searchString + '&method="' + data.searchMethod+'"')
            .then(function (response) {
                console.log('api/search?query=' + data.searchString + '&method="' + data.searchMethod + '"')
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
    var getBookmarks = function (data, callback) {
        fetch('api/bookmarks?id=' + data.id)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getWordCloudData = function (data, callback) {
        fetch("api/statistics/rankedwordlist?word='" + data.word+"'")
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
        fetch('api/bookmarks/'+data.userid, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(function (response) {
                console.log("response", response);
                return response.json();
            })
            .then(callback);
    };
    var deleteBookmark = function (data, callback) {
        fetch('api/bookmarks/' + data.postid, {
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

    var saveNote = function (data, callback) {
        fetch('api/notes/' + data.userid, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'content-type': 'application/json'
            }
        })
            .then(function (response) {
                console.log("response", response);
                return response.json();
            })
            .then(callback);
    };
    var deleteNote = function (data, callback) {
        fetch('api/notes/' + data.postid, {
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
        saveBookmark,
        getNotes,
        getBookmarks,
        deleteBookmark,
        getWordCloudData,
        saveNote,
        deleteNote

        
    }


});