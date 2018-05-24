define([], function () {
    return {
        menuList: [
            {
                name: "History",
                component: "history",
                params: { name: "History" },
                path: '/#/history'
            },
            {
                name: "Notes",
                component: "notes",
                params: { name: "Notes" },
                path: '/#/notes'
            }

        ]
    }
})