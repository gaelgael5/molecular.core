

import { RibbonPanel } from "./RibbonPanel";
 
/**
 * root model ribbon
 */
export class RibbonBar {

    /**
     * constructor
     */
    constructor() {
        this.Panels = new Array<RibbonPanel>();        
    }

    /**
     * List of panel
     */
    public Panels : RibbonPanel[];
    
}