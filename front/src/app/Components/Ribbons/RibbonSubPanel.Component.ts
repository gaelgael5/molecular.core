


import { Component, Input } from "@angular/core";
import { RibbonService } from "../../Models/Ribbons/RibbonService";
import { RibbonPanel } from "../../Models/Ribbons/RibbonPanel";
import { RibbonSubPanel } from "../../Models/Ribbons/RibbonSubPanel";
import { RibbonAction } from "../../Models/Ribbons/RibbonAction";
import { RibbonFormatSizeEnum } from "../../Models/Ribbons/RibbonFormatSizeEnum";

@Component({
    selector: 'ribbonsubpanel',
    templateUrl: './ribbonSubPanel.Component.html',

  })
export class RibbonSubPanelComponent {
    

    /**
     *
     */
    constructor() {

    }

    @Input() sub: RibbonSubPanel;

    public getClass(action : RibbonAction) : string {
        
       if (action.Size == RibbonFormatSizeEnum.List)
           return "btn btn-xs";

        else if (action.Size == RibbonFormatSizeEnum.Normal)
           return "btn btn-md";

        else if (action.Size == RibbonFormatSizeEnum.Small)
           return "btn btn-sm";

        return "";

    }

    
}