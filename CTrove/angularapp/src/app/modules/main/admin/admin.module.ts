import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AccessComponent } from '../../../pages/main/admin/access/access.component';
import { RolesComponent } from '../../../pages/main/admin/roles/roles.component';
import { UserComponent } from '../../../pages/main/admin/user/user.component';
import { InviteComponent } from '../../../pages/main/admin/invite/invite.component';
import { ReusableModule } from '../../reusable/reusable.module';
import { ManageUserComponent } from 'src/app/pages/main/admin/user/manage-user/manage-user.component';
import { ManageAccessComponent } from '../../../pages/main/admin/access/manage-access/manage-access.component';
import { ManageRolesComponent } from '../../../pages/main/admin/roles/manage-roles/manage-roles.component';

@NgModule({
  declarations: [
    AccessComponent,
    RolesComponent,
    UserComponent,
    InviteComponent,
    ManageUserComponent,
    ManageAccessComponent,
    ManageRolesComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReusableModule,
  ]
})
export class AdminModule { }
