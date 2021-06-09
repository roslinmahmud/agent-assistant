import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PreloadAllModules, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DatePipe } from '@angular/common';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './shared/guards/auth.guard';
import { VoltModule } from './volt/volt.module';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    VoltModule,
    RouterModule.forRoot(
      [
        { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
        { path: 'authentication', loadChildren: () => import('./authentication/authentication.module').then(m=>m.AuthenticationModule) },
        { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] }
      ],
      {preloadingStrategy: PreloadAllModules}
    ),
    JwtModule.forRoot({
      config:{
        tokenGetter: tokenGetter,
        whitelistedDomains: ["localhost:5001"],
        blacklistedRoutes:[]
      }
    })
  ],
  providers: 
  [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
