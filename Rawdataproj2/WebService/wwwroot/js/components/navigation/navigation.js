define(['knockout'], function (ko) {
    return function (params) {
        console.log('nag')
        // Data
        var self = this;
        this.menuItems = ['History', 'Search', 'Notes'];
        this.pageId = ko.observable();
        // Behaviours    
        self.goToPage = function (item) { self.chosenFolderId(item); };
        this.startSearch = function () {
            console.log('searching');
        }
        this.searchString = ko.observable();
        // public part
        return {
            searchString
        };
    };
});