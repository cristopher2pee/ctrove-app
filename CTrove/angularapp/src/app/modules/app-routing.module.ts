import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../pages/login/login.component';
import { OnboardingComponent } from '../pages/onboarding/onboarding.component';
import { PlaygroundComponent } from '../pages/common/dev/playground/playground.component';
import { MainGuard } from '../guards/main.guard';
import { RootGuard } from '../guards/root.guard';
import { OnboardingGuard } from '../guards/onboarding.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
    //loadChildren: () => import('./layout.module').then(m => m.LayoutModule)
  },
  {
    path: 'login',
    canActivate: [RootGuard],
    component: LoginComponent
  },
  {
    path: 'onboarding',
    component: OnboardingComponent
  },
  {
    path: 'play',
    component: PlaygroundComponent
  },
  { path: 'main', 
    canActivate: [MainGuard, OnboardingGuard], 
    loadChildren: () => import('./main/main.module').then((m) => m.MainModule) 
  },
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})

export class AppRoutingModule{}
