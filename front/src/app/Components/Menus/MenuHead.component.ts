
import { Component, Input, HostBinding } from "@angular/core";
import { MenuHead } from "../../Models/Menus/MenuHead";

@Component({
    selector: 'menuHead',
    templateUrl: './MenuHead.Component.html',
  })
export class MenuHeadComponent {

    constructor() {
        
        //console.log(

    }

    // @HostBinding() test : number;
    @Input() menu : MenuHead;

}