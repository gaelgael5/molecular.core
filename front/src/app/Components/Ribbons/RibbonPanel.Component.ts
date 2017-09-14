


import { Component, Input } from "@angular/core";
import { RibbonService } from "../../Models/Ribbons/RibbonService";
import { RibbonPanel } from "../../Models/Ribbons/RibbonPanel";

@Component({
    selector: 'ribbonpanel',
    templateUrl: './ribbonPanel.Component.html',

  })
export class RibbonPanelComponent {
    

    /**
     *
     */
    constructor() {

    }

    @Input() panel: RibbonPanel;

    
}