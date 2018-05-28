define(['knockout'], function (ko, param) {
    return function (params) {
        var notes = ko.observableArray();
        var id = 1;
        //fetch list of notes
        ko.computed(function () {
            req.getNotes({ id: id }, function (data) {
                console.log(data)
                notes(data.notes);
            });
        });



        return {
        };
    };
});
