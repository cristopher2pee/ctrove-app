import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConfigComponent } from 'src/app/pages/main/study/config/config.component';
import { SitesComponent } from 'src/app/pages/main/study/sites/sites.component';
import { SubjectsComponent } from 'src/app/pages/main/study/subjects/subjects.component';

const routes: Routes = [
  {
    path: '', 
    redirectTo: 'subjects',
    pathMatch: "full"
  },
  {
    path: 'subjects', 
    component: SubjectsComponent
  },
  {
    path: 'sites', 
    component: SitesComponent
  },
  {
    path: 'config', 
    component: ConfigComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StudyRoutingModule { }
