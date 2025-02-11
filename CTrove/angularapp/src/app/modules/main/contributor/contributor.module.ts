import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContributorsComponent } from '../../../pages/main/contributor/contributors/contributors.component';
import { OrganizationComponent } from '../../../pages/main/contributor/organization/organization.component';
import { ContributorRoutingModule } from './contributor-routing.module';
import { ReusableModule } from '../../reusable/reusable.module';
import { ManageOrganizationComponent } from '../../../pages/main/contributor/organization/manage-organization/manage-organization.component';
import { ManageContributorsComponent } from '../../../pages/main/contributor/contributors/manage-contributors/manage-contributors.component';

@NgModule({
  declarations: [
    ContributorsComponent, 
    OrganizationComponent, ManageOrganizationComponent, ManageContributorsComponent
  ],
  imports: [
    CommonModule, 
    ContributorRoutingModule, 
    ReusableModule],
})
export class ContributorModule {}
