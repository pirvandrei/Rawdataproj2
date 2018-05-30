define(['knockout', 'menu', 'sammy','request'],
    function (ko, menuDef, sammy,req) {
        this.searchString = ko.observable();
        var self = this;
        this.startSearch = function () {
        }
        var selectedMenu = ko.observable();
        var selectedComponent = ko.observable();
        var selectedParams = ko.observable();
        var selectedMethod = ko.observable();
        
        var Method = function (text, value) {
            this.text = text;
            this.value = value;
        };
         //Array containing available methods for search
        var availableMethods = ko.observableArray([
            new Method('Query weighted', 'bestmatchweighted'),
            new Method('Query ranked', 'bestmatchranked'),
            new Method('Query match all words', 'matchall'),
        ]);
        //When searching we will navigate to this route
        var startSearch = function () {
            //Save search when executed
            req.saveSearch({ text: searchString(), userid:1 }, function (data) {
                console.log('history saved');
            });
            
            window.location = '#/search' + '?selectedMethod=' + selectedMethod()+'&searchString='+searchString();
        }
        //we use this funciton to manually load certain components
        var loadComponent = function (data) {
            console.log(data);
            selectedComponent (data.component);
            selectedParams(data.params);
        }
        var changeMenu = function (menu) {
            selectedMenu(menu.name);
            selectedParams(menu.params);
            selectedComponent(menu.component);
        }

        var isActive = function (menu) {
            if (selectedMenu() === menu.name) {
                return "active";
            }
            return "";
        }
        // create the router
        var appRouter = sammy('#main', function () {
            var router = this;
            router.get("/", function (context) {
                loadComponent({ component: 'display-search', params: {searchString:'sql',searchMethod:'bestmatchweighted'} })

            });
            router.get("/#/history", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);

            });
            router.get("/#/notes", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);
            });
            router.get("/#/bookmarks", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);
            });
            router.get("/#/bookmarks", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);
            });
            //Route to post display
            router.get("/#/stats", function (context) {
                loadComponent({ component: 'statistics', params: context.params })
            });
            router.get("/#/post/:id", function (context) {
                loadComponent({ component: 'post', params: context.params })
            });
             //Route to search
            router.get("/#/search", function (context) {
                console.log(context.params)
                loadComponent({ component: 'display-search', params: context.params })
            });

        });
        
        ko.router = appRouter;

        appRouter.run();
        // select this default menu
        return {
            selectedComponent,
            selectedParams,
            menuList: menuDef.menuList,
            isActive,
            availableMethods,
            selectedMethod,
            startSearch
        }


    });