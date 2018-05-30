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
            paginate: 'js/services/paginate',

        }
    });
   
    require(['knockout'], function (ko) {
        ko.components.register("navigation", {
            viewModel: { require: "js/components/navigation/navigation" },
            template: { require: "text!js/components/navigation/navigation.html" }
        });
        ko.components.register("post", {
            viewModel: { require: "js/components/display-post/post" },
            template: { require: "text!js/components/display-post/post.html" }
        });
        ko.components.register("post-answer", {
            viewModel: { require: "js/components/display-post/postAnswer" },
            template: { require: "text!js/components/display-post/postAnswer.html" }
        });
        ko.components.register("post-question", {
            viewModel: { require: "js/components/display-post/postQuestion" },
            template: { require: "text!js/components/display-post/postQuestion.html" }
        });
        ko.components.register("post-comment", {
            viewModel: { require: "js/components/display-post/postComment" },
            template: { require: "text!js/components/display-post/postComment.html" }
        });
        ko.components.register("display-search", {
            viewModel: { require: "js/components/display-search/resultList" },
            template: { require: "text!js/components/display-search/resultList.html" }
        });
        ko.components.register("history", {
            viewModel: { require: "js/components/history/history" },
            template: { require: "text!js/components/history/history.html" }
        });
        ko.components.register("history-item", {
            viewModel: { require: "js/components/history/historyItem" },
            template: { require: "text!js/components/history/historyItem.html" }
        });
        ko.components.register("search-list-element", {
            viewModel: { require: "js/components/display-search/listElement" },
            template: { require: "text!js/components/display-search/listElement.html" }
        });
        ko.components.register("notes", {
            viewModel: { require: "js/components/notes/notes" },
            template: { require: "text!js/components/notes/notes.html" }
        });
        ko.components.register("note-item", {
            viewModel: { require: "js/components/notes/noteItem" },
            template: { require: "text!js/components/notes/noteItem.html" }
        });
        ko.components.register("bookmarks", {
            viewModel: { require: "js/components/bookmarks/bookmarks" },
            template: { require: "text!js/components/bookmarks/bookmarks.html" }
        });
        ko.components.register("statistics", {
            viewModel: { require: "js/components/statistics/statistics" },
            template: { require: "text!js/components/statistics/statistics.html" }
        });
        ko.components.register("wordcloud", {
            viewModel: { require: "js/components/statistics/wordcloud" },
            template: { require: "text!js/components/statistics/wordcloud.html" }
        });


    });

    require(['knockout', 'sammy', 'menu', 'js/viewModel'], function (ko, Sammy, menu,  vm) {
        ko.applyBindings(vm);
    })  

})();


