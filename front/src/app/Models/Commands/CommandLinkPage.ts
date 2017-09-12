

import { ICommand } from "./ICommand.interface";

export class CommandLinkPage implements ICommand {


    CanExecute(object: any): boolean {
        return true;
        // throw new Error("Method not implemented.");
    }

    Execute(object: any) {



    }

    public Page : string;

}