import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Volt } from './interfaces/volt';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient) { }

    public create = (route:string, body:Volt) => {
      return this.http.post('https://localhost:5001/' + route, body, this.generateHeaders())
    }

    public get = (route: string) => {
      return this.http.get('https://localhost:5001/'+route);
    }

    private generateHeaders = () => {
      return {
        headers: new HttpHeaders({'Content-Type': 'application/json'})
      }
    }
}
