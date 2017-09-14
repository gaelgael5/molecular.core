

/**
 * 
 * 
 */
import { RibbonSubPanel } from "./RibbonSubPanel";

export class RibbonPanel {

    /**
     *
     */
    constructor(name : string = null, panels : Array<RibbonSubPanel> = null) {

        this.Name = name;
        this.Panels = new Array<RibbonSubPanel>();

        if (panels != undefined)
        {

            // this.Panels.push(panels);
            
            panels.forEach(element => {
                this.Panels.push(element);
            });

        }

    }

    // public Add (panel : RibbonSubPanel) :RibbonPanel {

    //     this.Panels.push(panel);
    //     return this;
    // }

    /**
     * return list of sub panel
     */
    public Panels : Array<RibbonSubPanel>;
    public Display: string;
    public Name: string;
    
}