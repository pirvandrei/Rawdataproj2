define(['knockout', 'request', 'jquery', 'jqcloud'], function (ko, req, $, jQCloud) {
    return function (params) {
        var cloudData = ko.observable();
        var wordCloud = ko.observable("wordCloud");
        ko.computed(function () {
            req.getWordCloudData({ word: 'sql' }, function (data) {
                //console.log(data);
                //cloudData(data);
                //console.log(cloudData());
                //console.log(data);

                var words = ko.unwrap(data) || [];
                var width =   500;
                var height =   500;
                console.log(words);
                $('#wordcloud-container').jQCloud(words, {
                    width,
                    height,
                    shape: 'rectangular',
                    autoResize: true,
                    classPattern: null,
                    colors: ["#237204", "#38801d", "#4e8e36", "#659c4f", "#7baa68", "#91b881", "#a7c69a"],
                    fontSize: {
                        from: 0.1,
                        to: 0.03
                    },
                    delay: 50
                });

                $('#updatecloud').on('click', function () {
                    words.splice(-5);
                    $('#wordcloud-container').jQCloud('update', words);
                });
            });

        });

        return {
            wordCloud
        };
    };
});
