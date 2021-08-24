import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, isDevMode } from '@angular/core';
import { Vault } from '../../interfaces/vault';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient) { }

    public create<Type>(route:string, body:Type){
      return this.http.post(route, body, this.generateHeaders());
    }

    public update = (route:string, body:Vault) => {
      return this.http.put(route, body, this.generateHeaders());
    }

    public get = (route: string) => {
      return this.http.get(route);
    }

    private generateHeaders = () => {
      return {
        headers: new HttpHeaders({'Content-Type': 'application/json'})
      }
    }
}
