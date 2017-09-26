import {Component} from '@angular/core';
// import { TreeModel } from 'angular-tree-component';

@Component({
    selector:"console",
    templateUrl: './console.component.html'
})
export class consoleComponent {

    constructor() {

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

        
        //   {
        //     name: 'Actors',
        //     children: [  
        //     ]
        // },

        // this.nodes = [
        //     {
        //       name: 'North America',
        //       children: [
        //         { name: 'United States', children: [
        //           {name: 'New York'},
        //           {name: 'California'},
        //           {name: 'Florida'}
        //         ] },
        //         { name: 'Canada' }
        //       ]
        //     },
        //     {
        //       name: 'South America',
        //       children: [
        //         { name: 'Argentina', children: [] },
        //         { name: 'Brazil' }
        //       ]
        //     },
        //     {
        //       name: 'Europe',
        //       children: [
        //         { name: 'England' },
        //         { name: 'Germany' },
        //         { name: 'France' },
        //         { name: 'Italy' },
        //         { name: 'Spain' }
        //       ]
        //     }
        //   ];

        // this.nodes.nodes = [];

    }

    nodes: any;



}


        // this.AppendActors(application);
        // this.AppendServers(application);
        // this.AppendStructures(application);
        // this.AppendActions(application);
        // this.AppendWorkflows(application);
        // this.AppendTranslations(application);
        // this.AppendIHM(application);
        // this.AppendSecurity(application);


    // AppendActors(node : TreeNode) {

    //     var s = new TreeNode("Actors", "Actors", null);
    //     node.children.push(s);

    //     // s.Add(new TreeNode("act_vendor", "Vendeur", null));
    //     // s.Add(new TreeNode("act_manager", "manager", null));

    // }

    // AppendServers(node : treenodeModel) {
        
    //     var s = new treenodeModel("Servers", "Servers", null);
    //     node.Add(s);

    //     s.Add(new treenodeModel("srv_mongo1", "mongo1", null));
        
    // }
     
    // AppendStructures(node : treenodeModel) {
        
    //     var s = new treenodeModel("Structures", "Structures", null);
    //     node.Add(s);

    //     var flux = new treenodeModel("Flux", "Flux", null);
    //     var models = new treenodeModel ("Models", "Models", null);
    //     var services = new treenodeModel("Services", "Services", null);
        
    //     s.Add(flux)
    //         .Add(models)
    //         .Add(services);
     
    //     flux.Add(new treenodeModel("flux_application_1", "Application 1", null)
    //         .Add(new treenodeModel("flx_1", "Flux 1", null))
    //     );

    //     models.Add(new treenodeModel("model_account", "Account", null)
    //         .Add(new treenodeModel("prop_name", "Name", null))
    //         .Add(new treenodeModel("status_name", "Status", null))
    //     );

    //     services.Add(new treenodeModel("Expos_model_account", "Exposition du model account", null)
    //         .Add(new treenodeModel("flx_1", "Flux 1", null))
    //     );

    // }

    // AppendActions(node : treenodeModel) {
    //     var a = new treenodeModel("Actions", "actions", null);
    //     node.Add(a);
    // }

    // AppendWorkflows(node : treenodeModel) {
    //     var w = new treenodeModel("Workflows", "Workflows", null);
    //     node.Add(w);

    //     w.Add(new treenodeModel("wrk_1", "Processus 1", null));
    //     w.Add(new treenodeModel("wrk_2", "Processus 2", null));

    // }

    // AppendTranslations(node : treenode) {
    //     var t = new treenodeModel("Translations", "Traductions", null);
    //     node.Add(t);

    //     t.Add(
    //         new treenodeModel("Languages", "languages", null)
    //             .Add(new treenodeModel("fra", "Francais", null))
    
    //     );

    //     t.Add(
    //         new treenodeModel("Blk_civility", "Civilités", null)
    //             .Add(new treenodeModel("name", "Name", null))
    
    //     );


    // }

    // AppendIHM(node : treenode) {
        
    //     var ihm = new treenodeModel("ihm", "IHM", null);
    //     node.Add(ihm);

    //     ihm.Add(
    //         new treenodeModel("Components", "Components", null)
    //             .Add(new treenodeModel("cmp_infos_generales", "Infos générales", null))    
    //     );

    //     ihm.Add(
    //         new treenodeModel("Screens", "Ecrans", null)
    //             .Add(new treenodeModel("scr_1", "screen 1", null))    
    //     );

    // }

    // AppendSecurity(node : treenode) {
    //     var s = new treenodeModel("Security", "Security", null);
    //     node.Add(s);

    //     s.Add(
    //         new treenodeModel("Roles", "Roles", null)
    //             .Add(new treenodeModel("role_admin", "Administrateur", null))    
    //     );

    //     s.Add(
    //         new treenodeModel("Permissions", "Permissions", null)
    //             .Add(new treenodeModel("permission_name", "permission name", null))    
    //     );

    // }