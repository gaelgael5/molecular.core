


import { Router }       from '@angular/router';

export class ContextCommand {
    
    Router: Router;

    /**
     * constructor
     */
    constructor(private router: Router) {
        
        this.Router = router;

    }

    public Instance : any;
    
}