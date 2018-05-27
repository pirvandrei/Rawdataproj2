define([], function () {

    var getResults = function (callback) {
        fetch("api/persons")
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };
    var getPost = function (params,callback) {
        fetch("api/questions/"+params.id)
            .then(function (response) {
                return response.json();
            })
            .then(callback);
    };





    return {
        getPost,
        getResults
    }


});