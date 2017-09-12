

import { Injectable }   from '@angular/core'
import { Router }       from "@angular/router";
import { ContextCommand } from "./ContextCommand";

/**
 * provide command context that be provided in the command
 */
@Injectable()
export class ContextCommandProvider {

    /**
     * 
     * constructor
     * 
     * @param router injected router instance
     * 
     */
    constructor(router : Router)
    {
        this.router = router;
    }

    /**
     * provide context command
     */
    public GetContext() : ContextCommand {

        let context = new ContextCommand(this.router);

        return context;

    }
    
    router: Router;
    
}