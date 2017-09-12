

/**
 * class that define a graphic menu
 */
export class Menu {


    /**
     * Constructor
     * @param name display that be shown the screen
     * @param items list of sub menus
     */
    constructor(name : string, items : Menu[]) {
        this.Name = name;
        this.Menus = items;
    }

    public Type : string;
    
    public Name : string;

    public Menus : Array<Menu>;

}