define([], function () {

    var getResults = function (callback) {
        fetch("api/persons")
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getPost = function (callback) {
        fetch("api/persons")
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getResults = function (callback) {
        fetch("api/persons")
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getResults = function (callback) {
        fetch("api/persons")
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };




    return {
        getPosts
    }


});