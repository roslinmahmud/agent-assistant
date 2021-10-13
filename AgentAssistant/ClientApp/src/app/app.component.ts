import { Component, OnInit } from '@angular/core';
import { SignalRService } from './shared/services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{

  constructor(private signalR: SignalRService){ }

  ngOnInit(): void {
  }
}
