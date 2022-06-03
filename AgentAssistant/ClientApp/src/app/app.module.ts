import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DatePipe } from '@angular/common';
import { ErrorHandlerService } from './shared/services/error-handler.service';
import { JwtModule } from "@auth0/angular-jwt";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { AppRoutingModule } from './app-routing.module';

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    LoadingBarHttpClientModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config:{
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5001", "ibbl.azurewebsites.net"],
        disallowedRoutes:[]
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
