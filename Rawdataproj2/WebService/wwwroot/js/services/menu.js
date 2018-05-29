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
            },
            {
                name: "Bookmarks",
                component: "bookmarks",
                params: { name: "Bookmarks" },
                path: '/#/bookmarks'
            },
            {
                name: "Stats",
                component: "statistics",
                params: { name: "Statistics" },
                path: '/#/stats'
            }

        ]
    }
})