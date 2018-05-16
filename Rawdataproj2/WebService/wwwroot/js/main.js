(function () {
    require.config({
        baseUrl: '',
        paths: {
            knockout: 'bower_components/knockout/dist/knockout',
            jquery: 'bower_components/jQuery/dist/jquery.min',
            text: 'bower_components/requirejs-text/text',
            jqcloud: 'bower_components/jqcloud2/dist/jqcloud'
        }
    });
   
    require(['knockout'], function (ko) {
        ko.components.register("navigation", {
            viewModel: { require: "js/components/navigation/navigation" },
            template: { require: "text!js/components/navigation/navigation.html" }
        });
    });
    require(['knockout'], function (ko) {

        function appviewmodel() {
            this.firstname = ko.observable("bert");
            this.lastname = ko.observable("bertington");
        }
        ko.applyBindings(new appviewmodel());
    })  

})();

