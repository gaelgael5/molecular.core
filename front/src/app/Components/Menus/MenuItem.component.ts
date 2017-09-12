
import { Component, Input } from "@angular/core";
import { MenuItem } from "../../Models/Menus/MenuItem";
import { ContextCommandProvider } from "../../Models/Commands/ContextCommandProvider";
import { ContextCommand } from "../../Models/Commands/ContextCommand";

@Component({
    selector: 'menuItem',
    templateUrl: './MenuItem.Component.html',
  })
export class MenuItemComponent {
    

    constructor(contextProvider : ContextCommandProvider) {
        
        this.contextProvider = contextProvider;
    }

    public Run(menu1 : MenuItem)
    {
        let  contextCommand : ContextCommand =  this.contextProvider.GetContext();
        menu1.Command.Execute(contextCommand);
    }

    @Input() menu : MenuItem;
    contextProvider: ContextCommandProvider;
    
}