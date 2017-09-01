
export interface ICommand {
    
    CanExecute (object : any) : boolean;
    
    Execute (object : any);

}