(function () {
    require.config({
        baseUrl: '',
        paths: {
            knockout: 'bower_components/knockout/dist/knockout',
            jquery: 'bower_components/jQuery/dist/jquery.min',
            text: 'bower_components/requirejs-text/text',
            jqcloud: 'bower_components/jqcloud2/dist/jqcloud',
            sammy: 'bower_components/sammy/lib/sammy',
            menu: 'js/services/menu',
            request: 'js/services/request',
            state: 'js/services/state'
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
        ko.components.register("history", {
            viewModel: { require: "js/components/history/history" },
            template: { require: "text!js/components/history/history.html" }
        });
        ko.components.register("notes", {
            viewModel: { require: "js/components/notes/notes" },
            template: { require: "text!js/components/notes/notes.html" }
        });

    });

    require(['knockout', 'sammy', 'menu', 'state', 'js/viewModel'], function (ko, Sammy, menu, state, vm) {
        ko.applyBindings(vm);
    })  

})();


