import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { CommonModule, DatePipe, registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from '../pages/app.component';
import { MSAL_INSTANCE, MSAL_INTERCEPTOR_CONFIG, MsalInterceptor, MsalModule, MsalService } from '@azure/msal-angular';
import { MSALInterceptorConfigFactory, MSAL_InstanceFactory } from 'src/Utilities/common/keys/ms-auth';
import { ReusableModule } from './reusable/reusable.module';
import { LoginComponent } from '../pages/login/login.component';
import { UnauthorizedComponent } from '../pages/common/error/unauthorized/unauthorized.component';
import { PlaygroundComponent } from '../pages/common/dev/playground/playground.component';
import { OnboardingComponent } from '../pages/onboarding/onboarding.component';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
import { TokenInterceptorService } from '../services/token-interceptor.service';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UnauthorizedComponent,
    PlaygroundComponent,
    OnboardingComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    ReusableModule,
    MsalModule,    
  ],
  providers: [
    { 
      provide: NZ_I18N, 
      useValue: en_US 
    },
    {
      provide: MSAL_INSTANCE,
      useFactory: MSAL_InstanceFactory
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
       multi: true
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory
    },
    { 
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    { 
      provide: JWT_OPTIONS, 
      useValue: JWT_OPTIONS 
    },
    MsalService,
    JwtHelperService,
    DatePipe
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
