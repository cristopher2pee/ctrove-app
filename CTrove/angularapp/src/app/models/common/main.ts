export interface SIDE_MENU {
    name: string,
    icon: string | null,
    path: string
    subMenu: SIDE_MENU[] | null,
    isSelected: boolean
}

export interface NAV_LINK {
    name: string,
    path: string
}