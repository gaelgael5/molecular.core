
import { Menu } from "./Menu";

export class MenuHead extends Menu {

    constructor(name : string, items : Menu[]) {

        super(name, items)
        this.Type = 'menuHead';
    
    }

}