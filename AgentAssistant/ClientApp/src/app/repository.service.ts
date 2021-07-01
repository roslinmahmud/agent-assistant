import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Volt } from './interfaces/volt';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  baseURL:string = "https://localhost:5001/";

  constructor(private http: HttpClient) { }

    public create<Type>(route:string, body:Type){
      return this.http.post(this.baseURL + route, body, this.generateHeaders());
    }

    public update = (route:string, body:Volt) => {
      return this.http.put(this.baseURL+route, body, this.generateHeaders());
    }

    public get = (route: string) => {
      return this.http.get(this.baseURL+route);
    }

    private generateHeaders = () => {
      return {
        headers: new HttpHeaders({'Content-Type': 'application/json'})
      }
    }
}
