define(['knockout', 'menu', 'state', 'sammy'],
    function (ko, menuDef, state, sammy) {
        this.searchString = ko.observable();
        var self = this;
        this.startSearch = function () {
        }
        var selectedMenu = ko.observable();
        var selectedComponent = ko.observable();
        var selectedParams = ko.observable();

        //we use this funciton to manually load certain components
        var loadComponent = function (data) {
            
            selectedComponent = ko.observable(data.component);
            selectedParams = ko.observable(data.params);
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
        });
        
        ko.router = appRouter;

        appRouter.run();
        // select this default menu
        return {
            selectedComponent,
            selectedParams,
            menuList: menuDef.menuList,
            isActive
        }


    });