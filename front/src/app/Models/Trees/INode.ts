import { ICommand } from "../Commands/ICommand.interface";


/**
 * define node for tree view
 */
 export interface INode {

    /**
     *  Displayed text
     */
    name : string
    
    /**
     * children nodes
     */
    children: Array<INode>

    command : ICommand


}