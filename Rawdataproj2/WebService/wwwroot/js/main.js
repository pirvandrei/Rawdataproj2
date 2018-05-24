(function () {
    require.config({
        baseUrl: '',
        paths: {
            knockout: 'bower_components/knockout/dist/knockout',
            jquery: 'bower_components/jQuery/dist/jquery.min',
            text: 'bower_components/requirejs-text/text',
            jqcloud: 'bower_components/jqcloud2/dist/jqcloud',
            sammy: 'bower_components/sammy/lib/sammy'
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

    });
    require(['knockout','sammy'], function (ko,Sammy) {
        var self = this;
        self.locations = ['display-search'];
        self.currentLocation = ko.observable();

        // setup routing

        self.changeLocation = function (locaiton) { self.chosenFolderId(folder); };
        function vm() {
            var self = this;
            //Initially loaded comoponent
            this.componentName = ko.observable("post-items");
            this.searchString = ko.observable('');
            //Available pages in the menu
            this.menuItems = [
                { 'name': 'History', 'component': 'history' },
                { 'name': 'Notes', 'component': 'display-search' }
            ];
            this.pageId = ko.observable();
            // Behaviours    
            self.goToPage = function (item) {
                self.currentLocation(item.component);
                location.hash = item.component
            };
            //Execute this function to retrieve results for search
            this.startSearch = function () {
                
            }
            Sammy(function () {
                this.get(':item', function () {
                    console.log('sdas')
                    self.componentName(this.);
                    
                });
                this.notFound = function () {
                    self.componentName('display-search');
                }



                //this.get('#:folder/:mailId', function () {
                //    self.chosenFolderId(this.params.folder);
                //    self.chosenFolderData(null);
                //    $.get("/mail", { mailId: this.params.mailId }, self.chosenMailData);
                //});
            }).run();
        }
        ko.applyBindings(new vm());

    })  

})();


