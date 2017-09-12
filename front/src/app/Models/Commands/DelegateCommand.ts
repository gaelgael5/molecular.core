

import { ICommand } from "./ICommand.interface";
import { Predicate } from "@angular/core/core";
import { ContextCommand } from "./ContextCommand";

/**
 * define a command action that be executed everywhere
 */
export class DelegateCommand<T> implements ICommand {
    
    
    private execute: (context : ContextCommand) => void;
    private canExecute: (context : ContextCommand) => boolean;
    
    /**
     * 
     * constructor
     * 
     * @param execute       is the fonction must be ran
     * @param canExecute    is a function must be ran for evaluate if the command is valid for a specific use case defined by context command
     */
    constructor(execute: (context : ContextCommand) => void, canExecute: (context : ContextCommand) => boolean) {

        this.execute = execute;
        this.canExecute = canExecute;
    }

    /**
     * 
     * @param object is the instance context
     */
    public CanExecute(context: ContextCommand): boolean {
        let result : boolean = this.canExecute(context);

        return result;
    }

    /**
     * Execute action with the specified context command
     * 
     * @param object is the instance context
     */
    Execute(context: ContextCommand) {
        this.execute(context);
    }

}