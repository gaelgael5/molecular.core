import { Component } from "@angular/core";
import { MenuHead } from "../../Models/Menus/MenuHead";
import { MenuService } from "../../Models/Menus/MenuService";

@Component({
    selector: 'menus',
    templateUrl: './Menus.Component.html',

  })
export class MenusComponent {
    
    Menus: MenuHead[];

    constructor(menuApi : MenuService) {

        this.Menus = menuApi.GetMenus();

    }

}