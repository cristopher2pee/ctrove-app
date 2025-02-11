
export class Menu {
    id : string;
    icon : string;
    theme : any;
    shortName : string;
    name : string;
    router : string;
    href: string;
    exact : boolean;
    cssClass : string;
    target : string;
    note : string;
    img : string;
    noActive : boolean;
    submenu : Menu[];

    constructor(
        {
            id = '',
            icon = '',
            theme = 'outline',
            shortName = '',
            name = '',
            router= '',
            href= '',
            exact = false,
            cssClass= '',
            target = '',
            note = '',
            img = '',
            noActive = false,
            submenu = []
        }
    ){
        this.id = id;
        this.icon = icon;
        this.theme = theme;
        this.shortName = shortName;
        this.name = name;
        this.router = router;
        this.href = href;
        this.exact = exact;
        this.cssClass = cssClass;
        this.target = target;
        this.note = note;
        this.img = img;
        this.noActive = noActive;
        this.submenu = submenu
    }
}

