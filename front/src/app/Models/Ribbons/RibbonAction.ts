
import { RibbonFormatSizeEnum } from "./RibbonFormatSizeEnum";

export class RibbonAction {

     /**
      *
      */
     constructor(name: string  = null) {
            
        this.Name = name;
        
     }

    public Name: string;
    
    public Display: string;
    
    public Glyph: string;
    
    public Size : RibbonFormatSizeEnum;

}
