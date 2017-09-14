import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router';
import { rootRouterConfig } from './app.routes';
import { AppComponent } from './app.component';
import { GithubService } from './github/shared/github.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AboutComponent } from './about/about.component';
import { HomeComponent } from './home/home.component';
import { RepoBrowserComponent } from './github/repo-browser/repo-browser.component';
import { RepoListComponent } from './github/repo-list/repo-list.component';
import { RepoDetailComponent } from './github/repo-detail/repo-detail.component';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { ContactComponent } from './contact/contact.component';

import { MenusComponent } from "./Components/Menus/Menus.component";
import { MenuHeadComponent } from "./Components/Menus/MenuHead.component";
import { MenuItemComponent } from "./Components/Menus/MenuItem.component";
import { MenuService } from "./Models/Menus/MenuService";
import { ContextCommandProvider } from "./Models/Commands/ContextCommandProvider";
import { CommandRepository } from "./Models/Commands/CommandRepository";
import { RibbonService } from "./Models/Ribbons/RibbonService";
import { RibbonComponent } from "./Components/Ribbons/Ribbon.Component";
import { RibbonPanelComponent } from "./Components/Ribbons/RibbonPanel.Component";
import { RibbonSubPanelComponent } from "./Components/Ribbons/RibbonSubPanel.Component";

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    RepoBrowserComponent,
    RepoListComponent,
    RepoDetailComponent,
    HomeComponent,
    ContactComponent,

    MenusComponent,
    MenuHeadComponent,
    MenuItemComponent,

    RibbonComponent,
    RibbonPanelComponent,
    RibbonSubPanelComponent,

  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    RouterModule.forRoot(rootRouterConfig, { useHash: true })
  ],
  providers: [
    GithubService,
    
    MenuService,
    RibbonService,
    
    CommandRepository,
    ContextCommandProvider,

  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {

}
