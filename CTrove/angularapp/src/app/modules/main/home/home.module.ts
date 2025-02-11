import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { DashboardComponent } from 'src/app/pages/main/home/dashboard/dashboard.component';
import { ReusableModule } from '../../reusable/reusable.module';


@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    ReusableModule
  ]
})
export class HomeModule { }
