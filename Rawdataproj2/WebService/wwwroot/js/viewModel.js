define(['knockout', 'menu', 'state', 'sammy','request'],
    function (ko, menuDef, state, sammy,req) {
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
            new Method('Ranked', 'bestmatchranked'),
            new Method('Weighted', 'bestmatchweighted'),
            new Method('All', 'bestmatchall'),
        ]);
        //When searching we will navigate to this route
        var startSearch = function () {
            console.log({ text: searchString(),user: 1 })
            req.saveSearch({ text: searchString(), user:1 }, function (data) {
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

            router.get("/#/history", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);

            });
            router.get("/#/notes", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                changeMenu(menu);
            });
            //Route to post display
            router.get("/#/post/:id", function (context) {
                loadComponent({ component: 'post', params: context.params })
            });
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