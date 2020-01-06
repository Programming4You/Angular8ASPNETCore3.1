import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderComponent } from './orders/order/order.component';
import { OrdersComponent } from './orders/orders.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { ForbiddenComponent } from './auth/forbidden/forbidden.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { HomeComponent } from './home/home.component';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';


const routes: Routes = [
  {path:'', redirectTo:'user/login', pathMatch:'full'},
  {
    path:'user', component:UserComponent, 
    children:[
      {path:'login', component:LoginComponent},
      {path:'registration', component:RegistrationComponent}
    ]
  },
  {path:'home', component:HomeComponent, canActivate:[AuthGuard], children: [
    {path:'orders', component:OrdersComponent, canActivate:[AuthGuard]},
    {path:'order', 
       children:[
          {path:'', component:OrderComponent },
          {path:':id', component:OrderComponent }  
     ]},
    {path:'employee', component:EmployeeListComponent, canActivate:[AuthGuard]},
    {path:'forbidden', component:ForbiddenComponent },
    {path:'admin-panel', component:AdminPanelComponent, canActivate:[AuthGuard], data: {permittedRoles:['Admin']} }
  ]},
  {path:'**', redirectTo:'home', canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
