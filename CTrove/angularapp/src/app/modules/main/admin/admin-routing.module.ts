import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccessComponent } from 'src/app/pages/main/admin/access/access.component';
import { RolesComponent } from 'src/app/pages/main/admin/roles/roles.component';
import { UserComponent } from 'src/app/pages/main/admin/user/user.component';

const routes: Routes = [
  {
    path: '', 
    redirectTo: 'access',
    pathMatch: "full"
  },
  {
    path: 'access', 
    component: AccessComponent
  },
  {
    path: 'roles', 
    component: RolesComponent
  },
  {
    path: 'user', 
    component: UserComponent
  },
  // {
  //   path: 'invite', 
  //   component: InviteComponent
  // },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
