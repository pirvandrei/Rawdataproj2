define(['knockout'],
    function (ko, postman, store, config) {


        var selectedElement = ko.observable();
        var selectedComponent = ko.observable();
        var selectedParams = ko.observable();

        var changeMenu = function (menu) {
            selecteMenu(menu.name);
            selectedParams(menu.params);
            selectedComponent(menu.component);
        }

        postman.subscribe("ChangeMenu", changeMenu);

        //store.subscribe(function () {
        //    selecteMenu(store.getState().selectedMenu);
        //    selectedParams(store.getState().selectedParams);
        //    selectedComponent(store.getState().selectedComponent);
        //});

        var isActive = function (menu) {
            if (selecteMenu() === menu.name) {
                return "active";
            }
            return "";
        }

        // select this default menu
        //store.dispatch(store.actions.changeMenu(config.menuList[0]));
        changeMenu(config.menuList[0]);

        return {
            selectedComponent,
            selectedParams,
            menuList: config.menuList,
            isActive
        }


    });