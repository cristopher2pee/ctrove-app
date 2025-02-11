import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContributorsComponent } from 'src/app/pages/main/contributor/contributors/contributors.component';
import { OrganizationComponent } from 'src/app/pages/main/contributor/organization/organization.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'contributors',
    pathMatch: 'full',
  },
  {
    path: 'contributors',
    component: ContributorsComponent,
  },
  {
    path: 'organizations',
    component: OrganizationComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContributorRoutingModule {}
