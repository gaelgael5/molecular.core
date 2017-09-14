

import { Injectable } from "@angular/core";
import { CommandRepository } from "../Commands/CommandRepository";
import { RibbonBar } from "./RibbonBar";
import { RibbonPanel } from "./RibbonPanel";
import { RibbonSubPanel } from "./RibbonSubPanel";
import { RibbonAction } from "./RibbonAction";
import { RibbonFormatSizeEnum } from "./RibbonFormatSizeEnum";

@Injectable()
export class RibbonService {

/**
 *
 */
    constructor(commandRepository : CommandRepository) {
        
        this.commandRepository = commandRepository;
        
    }


    public GetRibbon() : RibbonBar {

            var ribbon = new RibbonBar();

            ribbon.Panels.push(
            {
                Display : "Home",
                Name    : "TabHome",
                Panels  : 
                [
                    new RibbonSubPanel([
                        { Display : "Copy", Size : RibbonFormatSizeEnum.List, Glyph : "glyphicon glyphicon-copy", Name : "" },
                        { Display : "Cut", Size : RibbonFormatSizeEnum.List, Glyph : "glyphicon glyphicon-remove", Name : "" },
                        { Display : "Past", Size : RibbonFormatSizeEnum.List, Glyph : "glyphicon glyphicon-paste", Name : "" },
                        { Display : "Special past", Size : RibbonFormatSizeEnum.Normal, Glyph : "glyphicon glyphicon-paste", Name : "" },
                        { Display : "Past 2", Size : RibbonFormatSizeEnum.List, Glyph : "glyphicon glyphicon-pencil", Name : "" },
                        { Display : "Search", Size : RibbonFormatSizeEnum.Normal, Glyph : "glyphicon glyphicon-search", Name : "" }
                    ]),

                    new RibbonSubPanel([
                        { Display : "Left align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-align-left", Name : "" },
                        { Display : "Center align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-align-center", Name : "" },
                        { Display : "justify align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-align-justify", Name : "" },
                        { Display : "Right align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-align-center", Name : "" },
                    ]),

                    new RibbonSubPanel([
                        { Display : "Top align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-object-align-top", Name : "" },
                        { Display : "Middle align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-object-align-right", Name : "" },
                        { Display : "Bottom align", Size : RibbonFormatSizeEnum.Small, Glyph : "glyphicon glyphicon-object-align-bottom", Name : "" },
                    ])

                ]
                });

            ribbon.Panels.push(
            {
                Display : "Insert",
                Name    : "TabInsert",
                Panels  : 
                [
                    new RibbonSubPanel([
                        { Display : "Copy", Size : RibbonFormatSizeEnum.List, Glyph : "", Name : "" },
                        { Display : "Cut", Size : RibbonFormatSizeEnum.List, Glyph : "", Name : "" },
                        { Display : "Past", Size : RibbonFormatSizeEnum.List, Glyph : "", Name : "" },
                        { Display : "Special past", Size : RibbonFormatSizeEnum.Normal, Glyph : "", Name : "" }
                    ]),
                
                    new RibbonSubPanel([
                        { Display : "Left align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                        { Display : "Center align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                        { Display : "Right align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                    ]),
                
                    new RibbonSubPanel([
                        { Display : "Top align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                        { Display : "Middle align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                        { Display : "Bottom align", Size : RibbonFormatSizeEnum.Small, Glyph : "", Name : "" },
                    ])
                
                ]
            });

            return ribbon;
    
        }


    private commandRepository : CommandRepository;
    
}