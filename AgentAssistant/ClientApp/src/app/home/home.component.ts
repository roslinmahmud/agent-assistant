import { Component, OnInit } from '@angular/core';
import { Summary } from '../interfaces/summary';
import { RepositoryService } from '../shared/services/repository.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public summary: Summary;

  constructor(private repository: RepositoryService){}

  ngOnInit(): void {
    this.getSummary();
  }

  private getSummary(){
    
    this.repository.get('/api/agent/summary/')
    .subscribe(
      res => {
        this.summary = res as Summary;
      },
      error => {
        console.log(error);
      }
    );
  }
}
