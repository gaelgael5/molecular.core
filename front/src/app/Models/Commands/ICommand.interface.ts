
import { ContextCommand } from "./ContextCommand";

export interface ICommand {
    
    CanExecute (context : ContextCommand) : boolean;
    
    Execute (context: ContextCommand);

}