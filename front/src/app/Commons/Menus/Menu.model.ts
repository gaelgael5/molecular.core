


import { Component } from "@angular/core";

@Component({
    selector: 'menu',
    templateUrl: 'Menu.html',
  })
export class Menu {

    constructor() {


    }

    public Name : string;

    public SubItems : Array<Menu>;

}