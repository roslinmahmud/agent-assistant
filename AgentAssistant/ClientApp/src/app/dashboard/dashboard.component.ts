import { Component, OnInit } from '@angular/core';
import { Summary } from '../interfaces/summary';
import { SignalRService } from '../shared/services/signal-r.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit{
  public summary: Summary;

  constructor(private signalR: SignalRService){}

  ngOnInit(): void {
    this.signalR.summaryChanged.subscribe(res =>{
      this.summary = res as Summary;
    })
  }

  

  

  
}
