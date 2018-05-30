define(['knockout', 'request', 'jquery', 'jqcloud'], function (ko, req, $, jQCloud) {
    return function (params) {
        var cloudData = ko.observable();

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
                    colors: ["#800026", "#bd0026", "#e31a1c", "#fc4e2a", "#fd8d3c", "#feb24c", "#fed976"],
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

        };
    };
});
