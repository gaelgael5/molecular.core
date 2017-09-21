/**
 * Menu data service
*/ 

import { Inject, Injectable, Optional }         from '@angular/core';
import { Menu } from "./Menu";
import { MenuItem } from "./MenuItem";
import { MenuHead } from "./MenuHead";
import { CommandRepository } from "../Commands/CommandRepository";
import { ContextCommand } from "../Commands/ContextCommand";

@Injectable()
export class MenuService {

    private commandRepository : CommandRepository;
    private menus : Array<Menu>;

    constructor(commandRepository : CommandRepository) {

        this.commandRepository = commandRepository;
    }

    /**
     * 
     * @param viewName 
     */
    public GetMenus(): Array<MenuHead> {        


        this.commandRepository.Method("GoToAbout", 
        (context : ContextCommand) => 
        {
            context.Router.navigate(["/about"]);
        },  
        (context : ContextCommand) => { return true; }
       );

       this.commandRepository.Method("GoToConsole", 
       (context : ContextCommand) => 
       {
           context.Router.navigate(["/console"]);
       },  
       (context : ContextCommand) => { return true; }
      );

        this.menus = new Array<MenuHead>();
        var m = new MenuHead("Applications 1", [

            new MenuItem("Console 1", this.commandRepository.Get("GoToConsole")),
            new MenuItem("Globalization 1",  null),
            new MenuItem("About", this.commandRepository.Get("GoToAbout")),

        ]);
        this.menus.push(m);

        var m2 = new MenuHead("Applications 2", [
            
                        new MenuItem("Console 2", null),
                        new MenuItem("Globalization 2", null),
            
                    ]);
                    this.menus.push(m2);

        return this.menus;
        
    }

}
