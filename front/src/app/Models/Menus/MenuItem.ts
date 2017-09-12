

import { Menu } from "./Menu";
import { ICommand } from "../Commands/ICommand.interface";


/**
 * derived class of Menu
 */
export class MenuItem extends Menu {

    /**
     * 
     * @param name display that be shown the screen
     * @param command  command must be launched
     */
    constructor(name: string, command : ICommand) {
    
        super(name, []);

        this.Command = command;

        this.Type = 'MenuItem';
        
    }

    /**
     * command to launch
     */
    public Command : ICommand;

}