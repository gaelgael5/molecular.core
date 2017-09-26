import {Component} from '@angular/core';
import { ContextCommandProvider } from '../../Models/Commands/ContextCommandProvider';
import { CommandRepository } from '../../Models/Commands/CommandRepository';

// import { TreeModel } from 'angular-tree-component';

@Component({
    selector:"console",
    templateUrl: './console.component.html'
})
export class consoleComponent {

    
    constructor(contextProvider : ContextCommandProvider, commandRepository : CommandRepository) {

        this.nodes = [
            {
                name: 'Configurations',
                children: [  
                    { name : "Configuration server" },                    
                  ] 
              },
            {
              name: 'Actors',
              children: [  
                  { name : "Vendeur" },
                  { name : "Manager" },
                ] 
            },
            {
              name: 'Structures',
              children: [
                {
                    name: 'Models',
                    children: [  
                        {
                            name: 'Account',
                            children: [  
                                { name: 'Name', },
                                { name: 'Status', },
                            ]
                        },
                    ]
                },
                { name: 'Services' },
                { name: 'Flux' },
              ]
            },
            {
              name: 'Actions',
              children: [
              ]
            },
            {
                name: 'Workflows',
                children: [  
                    { name: 'Processus 1', },
                    { name: 'Processus 2', },
                ]
            },
            {
                name: 'Traductions',
                children: [  
                    { name: 'Languages', children: [{ name : "Français" }] },
                    { name: 'Bundles', children: [{ name : "Bundle civilité" }] },
                ]
            },
            {
                name: 'IHM',
                children: [  
                    {
                        name: 'Component',
                        children: [  
                            {
                                name: 'Infos générales',
                                children: [  
                                ]
                            },
                    
                        ]
                    },
                    {
                        name: 'Screens',
                        children: [  
                            {
                                name: 'Screen 1',
                                children: [  
                                ]
                            },
                    
                        ]
                    },
            
                ]
            },
            {
                name: 'Security',
                children: [  
                    {
                        name: 'Roles',
                        children: [ 
                            { name: 'Role administrateur' },                     
                        ]
                    },
                    {
                        name: 'Permissions',
                        children: [  
                            {
                                name: 'Permission 1',
                                children: [  
                                ]
                            },
                    
                        ]
                    },
            
                ]
            },

        ];
          
        this.contextProvider = contextProvider;
        this.commandRepository = commandRepository;

    }

    nodes: any;
    private contextProvider: ContextCommandProvider;
    private commandRepository : CommandRepository;

}
