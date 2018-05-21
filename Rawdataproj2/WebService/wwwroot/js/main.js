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
        ko.components.register("post-items", {
            viewModel: { require: "js/components/display-post/postItems" },
            template: { require: "text!js/components/display-post/postItems.html" }
        });
        ko.components.register("post-answer", {
            viewModel: { require: "js/components/display-post/postAnswer" },
            template: { require: "text!js/components/display-post/postAnswer.html" }
        });
        ko.components.register("post-question", {
            viewModel: { require: "js/components/display-post/postQuestion" },
            template: { require: "text!js/components/display-post/postQuestion.html" }
        });
        ko.components.register("post-comments", {
            viewModel: { require: "js/components/display-post/postComments" },
            template: { require: "text!js/components/display-post/postComments.html" }
        });
        ko.components.register("display-search", {
            viewModel: { require: "js/components/display-search/resultList" },
            template: { require: "text!js/components/display-search/resultList.html" }
        });

        //ko.components.register("post-comment", {
        //    viewmodel: { require: "js/components/display-post/postcomment" },
        //    template: { require: "text!js/components/display-post/postcoment.html" }
        //});
        //ko.components.register("post-items", {
        //    viewmodel: { require: "js/components/display-post/postitems" },
        //    template: { require: "text!js/components/display-post/postitems.html" }
        //});

    });
    require(['knockout'], function (ko) {

        function appviewmodel() {
            this.firstname = ko.observable("bert");
            this.lastname = ko.observable("bertington");
        }
        ko.applyBindings(new appviewmodel());
    })  

})();

