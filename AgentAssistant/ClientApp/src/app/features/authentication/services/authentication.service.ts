import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject } from 'rxjs';
import { AuthResponse } from 'src/app/features/authentication/models/authResponse';
import { RegistrationResponse } from 'src/app/features/authentication/models/registrationResponse';
import { UserForAuthentication } from 'src/app/features/authentication/models/userForAuthentication';
import { UserForRegistration } from 'src/app/features/authentication/models/userForRegistration';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  constructor(private http: HttpClient,
    private jwtHelper:JwtHelperService) { }

  public registerUser = (route:string, body: UserForRegistration) => {
    return this.http.post<RegistrationResponse>(route, body);
  }

  public loginUser = (route:string, body: UserForAuthentication) => {
    return this.http.post<AuthResponse>(route, body);
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public logout = () => {
    localStorage.removeItem("token");
    this.sendAuthStateChangeNotification(false);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
    let isExpired = this.jwtHelper.isTokenExpired(token);
    return token && !isExpired;
  }

  public getAgentId = (): string => {
    const decoded = this.jwtHelper.decodeToken(localStorage.getItem("token"));
    return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
  }
}
