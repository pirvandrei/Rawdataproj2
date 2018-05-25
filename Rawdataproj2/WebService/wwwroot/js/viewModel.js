define(['knockout', 'menu', 'state', 'sammy'],
    function (ko, menuDef, state, sammy) {
        this.searchString = ko.observable();
        this.startSearch = function () {
        }
        var selectedMenu = ko.observable();
        var selectedComponent = ko.observable();
        var selectedParams = ko.observable();
        var changeMenu = function (menu) {
            selectedMenu(menu.name);
            selectedParams(menu.params);
            selectedComponent(menu.component);
            console.log(menu)
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
                console.log(menu)
                changeMenu(menu);

            });
            router.get("/#/notes", function (context) {
                var menu = menuDef.menuList.find(m => m.path == context.path);
                console.log(menu)
                changeMenu(menu);
            });
        });

        ko.router = appRouter;

        appRouter.run();
        // select this default menu
        changeMenu(menuDef.menuList[0]);
        console.log(selectedComponent);
        return {
            selectedComponent,
            selectedParams,
            menuList: menuDef.menuList,
            isActive
        }


    });