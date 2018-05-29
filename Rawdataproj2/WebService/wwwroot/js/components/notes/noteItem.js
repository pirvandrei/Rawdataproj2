define(['knockout'], function (ko, param) {
    return function (params) {
        // public part
        var text = params.text;
        var title = params.title;
        var url = params.url;
        var id = params.id;
        return {
            text,
            title,
            url,
            id
        };
    };
});
