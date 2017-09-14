

import { Component } from "@angular/core";
import { RibbonService } from "../../Models/Ribbons/RibbonService";
import { RibbonBar } from "../../Models/Ribbons/RibbonBar";

@Component({
    selector: 'ribbon',
    templateUrl: './ribbon.Component.html',

  })
export class RibbonComponent {
    

    /**
     *
     */
    constructor(provider : RibbonService) {
        this.ribbon = provider.GetRibbon();            
    }

    ribbon: RibbonBar;

    
}