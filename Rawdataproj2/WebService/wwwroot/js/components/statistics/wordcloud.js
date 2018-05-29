define(['knockout', 'request','jquery' ], function (ko, req,jq) {
    return function (params) {
        var cloudData = ko.observable();

        ko.computed(function () {
            req.getCloudData(data, function (data) {
                conosle.log(data)
                cloudData = data();
                jq.jQCloud(cloudData);
            });

        });

        return {

        };
    };
});
