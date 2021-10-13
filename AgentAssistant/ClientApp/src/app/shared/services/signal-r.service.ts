import { Injectable, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Summary } from 'src/app/interfaces/summary';
import { RepositoryService } from './repository.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnInit{

  hubConnection: HubConnection;
  private summaryChangeSub = new Subject<Summary>();
  public summaryChanged = this.summaryChangeSub.asObservable();

  constructor(private repository: RepositoryService) {
    this.startSignalRConnection();
   }

  ngOnInit(): void {
  }

  public startSignalRConnection = () => {
    this.hubConnection = new HubConnectionBuilder()
                            .withUrl('/summary')
                            .build();

    this.hubConnection
        .start()
        .then( () => {
          this.StartRequest();
          this.addTransferDashboardDataListener();
        })
  }

  private StartRequest(){
    this.repository.get('/api/agent/summary/')
    .subscribe(
      res => {
        console.log(res);
      },
      error => {
        console.log(error);
      }
    );
  }

  private addTransferDashboardDataListener(){
    this.hubConnection.on('transferSummaryData', (res) => {
      this.summaryChangeSub.next(res.value);
      console.log(res);
    })
  }
  
}
