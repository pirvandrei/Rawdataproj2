define(['knockout', 'menu','state'],
    function (ko, menuDef, state) {
        this.searchString = ko.observable();
        this.startSearch = function () {
        }
        var selecteMenu = ko.observable();
        var selectedComponent = ko.observable();
        var selectedParams = ko.observable();

        var changeMenu = function (menu) {
            selecteMenu(menu.name);
            selectedParams(menu.params);
            selectedComponent(menu.component);
        }


        var isActive = function (menu) {
            if (selecteMenu() === menu.name) {
                return "active";
            }
            return "";
        }

        // select this default menu
        changeMenu(menuDef.menuList[1]);

        return {
            selectedComponent,
            selectedParams,
            menuList: menuDef.menuList,
            isActive
        }


    });