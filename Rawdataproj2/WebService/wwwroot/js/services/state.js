define([], function () {
    const CHANGE_MENU = "CHANGE_MENU";

    var currentState = {};
    var reducer = (state, action) => {
        switch (action.type) {
            case CHANGE_MENU:
                return Object.assign(
                    {},
                    state,
                    {
                        selectedMenu: action.selectedMenu,
                        selectedComponent: action.selectedComponent,
                        selectedParams: action.selectedParams
                    });
            default:
                return state;
        }
    };

    var getState = () => currentState;

    var dispatch = (action) => {
        currentState = reducer(currentState, action);
    };

    var actions = {
        changeMenu: function (menu) {
            return {
                type: CHANGE_MENU,
                selectedMenu: menu.name,
                selectedComponent: menu.component,
                selectedParams: menu.params
            }
        }
    }

    return {
        getState,
        actions,
        dispatch
    }
});