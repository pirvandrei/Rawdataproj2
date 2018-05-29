define(['knockout', 'request', 'jquery', 'jqcloud'], function (ko, req, $, jQCloud) {
    return function (params) {
        var cloudData = ko.observable();

        ko.computed(function () {
            req.getWordCloudData({ word: 'sql' }, function (data) {
                console.log(data);
                cloudData(data);
                console.log(cloudData());
                console.log(data);
                $('#wordcloud-container').jQCloud(cloudData());
                
            });

        });

        return {

        };
    };
});
