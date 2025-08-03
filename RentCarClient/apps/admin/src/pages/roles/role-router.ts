import { Routes } from "@angular/router";

const router: Routes = [
    {
        path: '',
        loadComponent: () => import("./roles")
    },
    {
        path: 'add',
        loadComponent: () => import("./create/create")
    },
    {
        path: 'edit/:id',
        loadComponent: () => import("./create/create")
    },
    {
        path: 'detail/:id',
        loadComponent: () => import("./detail/detail")
    },
    {
        path: 'permissions/:id',
        loadComponent: () => import("./permissions/permissions")
    }
];

export default router;
