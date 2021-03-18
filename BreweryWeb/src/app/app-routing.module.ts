import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserListComponent } from './user/user-list/user-list.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path : '', component : HomeComponent },
  { path : 'user', component : UserComponent,
  children: [
    { path : 'login', component : LoginComponent },
    { path : 'registration', component : RegistrationComponent },
    { path : 'user-list', component : UserListComponent }
  ]
 }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
