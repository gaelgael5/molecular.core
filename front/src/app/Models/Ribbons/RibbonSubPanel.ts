
import { RibbonAction } from "./RibbonAction";

export class RibbonSubPanel {

    /**
     * contructor
     */
    constructor(actions : Array<RibbonAction> = null) {

        this.Actions =  new Array<RibbonAction>(); 
        if (actions != undefined) {

            actions.forEach(element => {
                this.Actions.push(element);
            });

        }

    }

    // public Add(actions : RibbonAction[]) : RibbonSubPanel {

    //     this.Actions.push(actions);
    //     return this;
    // }

    public Actions : Array<RibbonAction>;

}