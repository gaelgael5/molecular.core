

import { ICommand } from "../ICommand.interface";
import { Menu } from "./Menu.model";

export class MenuItem  extends Menu {

    constructor(command : ICommand) {
    
        super();

        this.Command = command;

    }

    public Command : ICommand;

}