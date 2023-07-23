import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailsComponent } from './components/members/member-details/member-details.component';
import { MessagesComponent } from './components/messages/messages.component';
import { ListsComponent } from './components/lists/lists.component';
import { AuthGuard } from './_guard/auth.guard';

const routes: Routes = [
  {path:'',component:HomeComponent},
  //dummy route (children)
  {path:'',runGuardsAndResolvers:'always',canActivate:[AuthGuard],children:[
    {path:'members',component:MemberListComponent},
    {path:'members/:id',component:MemberDetailsComponent},
    {path:'messages',component:MessagesComponent},
    {path:'lists',component:ListsComponent},
  ]},
 
  {path:'**',component:HomeComponent, pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
