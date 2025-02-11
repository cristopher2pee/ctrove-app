import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from 'src/app/pages/common/error/not-found/not-found.component';
import { ProfileComponent } from 'src/app/pages/main/common/profile/profile.component';
import { MainComponent } from 'src/app/pages/main/main.component';

const routes: Routes = [
  {
    path: '', 
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      },
      {
        path: 'profile',
        component: ProfileComponent
      },
      {
        path: 'home',
        loadChildren: () => import('../main/home/home.module').then((m) => m.HomeModule) 
      },
      {
        path: 'study',
        loadChildren: () => import('../main/study/study.module').then((m) => m.StudyModule) 
      },
      {
        path: 'admin',
        loadChildren: () => import('../main/admin/admin.module').then((m) => m.AdminModule) 
      },
      {
        path: 'contributor',
        loadChildren: () => import('../main/contributor/contributor.module').then((m) => m.ContributorModule) 
      },
      { path: "**", component: NotFoundComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
