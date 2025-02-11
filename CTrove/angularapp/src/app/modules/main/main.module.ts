import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainRoutingModule } from './main-routing.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { MainComponent } from 'src/app/pages/main/main.component';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { ReusableModule } from '../reusable/reusable.module';
import { NotFoundComponent } from '../../pages/common/error/not-found/not-found.component';
import { ProfileComponent } from '../../pages/main/common/profile/profile.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MainComponent,
    NotFoundComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    ReusableModule,
    ReactiveFormsModule,

    // NZ
    NzLayoutModule,
    NzBreadCrumbModule,
    NzMenuModule,
  ]
})
export class MainModule { }
