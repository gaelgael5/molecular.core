

import { ICommand } from "./ICommand.interface";
import { ContextCommand } from "./ContextCommand";
import { Predicate, Injectable } from "@angular/core";
import { DelegateCommand } from "./DelegateCommand";

/**
 * referential of commands
 */
@Injectable()
export class CommandRepository {
    

    private actions : { [name : string] : ICommand } = { };

    /**
     * constructor
     */
    constructor() {
    }

    /**
     *
     * Add a new command in the referential
     *  
     * @param name          name of the fonction
     * @param execute       is the fonction must be ran
     * @param canExecute    is a function must be ran for evaluate if the command is valid for a specific use case defined by context command
     */
    public Method(name : string, execute: (context : ContextCommand) => void, canExecute: Predicate<ContextCommand>) : ICommand {

        let cmd = new DelegateCommand(execute, canExecute);

        this.actions[name] = cmd;

        return cmd;
        
    }

    /**
     * 
     * @param name name of the command
     */
    public Get(name :string) : ICommand
    {
        return this.actions[name];
    }

    /**
     * 
     * @param name  name of the command
     */
    public Remove(name : string)
    {
        this.actions[name] = null;
    }


}


