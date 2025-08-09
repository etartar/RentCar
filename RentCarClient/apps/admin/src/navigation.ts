export interface NavigationModel{
    title: string;
    url?: string;
    icon?: string;
    haveSubNav?: boolean;
    subNavs?: NavigationModel[];
    permission: string;
}

export const navigations : NavigationModel[] = [
    {
        title: 'Dashboard',
        url: '/',
        icon: 'bi-speedometer2',
        permission: 'dashboard:view'
    },
    {
        title: 'Şubeler',
        url: '/branches',
        icon: 'bi-buildings',
        permission: 'branch:view'
    },
    {
        title: 'Roller',
        url: '/roles',
        icon: 'bi-clipboard2-check',
        permission: 'role:view'
    },
    {
        title: 'Kullanıcılar',
        url: '/users',
        icon: 'bi-people',
        permission: 'user:view'
    },
    {
        title: 'Kategoriler',
        url: '/categories',
        icon: 'bi-tags',
        permission: 'category:view'
    },
    {
        title: 'Güvence Paketleri',
        url: '/protection-packages',
        icon: 'bi-shield-check',
        permission: 'protection_package:view'
    },
    {
        title: 'Ekstralar',
        url: '/extras',
        icon: 'bi-plus-square',
        permission: 'extra:view'
    }
];
