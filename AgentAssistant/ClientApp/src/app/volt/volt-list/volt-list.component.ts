import { Component, OnInit } from '@angular/core';
import { Volt } from '../../interfaces/volt';
import { RepositoryService } from '../../repository.service';

import { DatePipe, registerLocaleData } from '@angular/common';
import  localeBn from '@angular/common/locales/bn';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

registerLocaleData(localeBn, 'bn');

@Component({
  selector: 'app-volt-list',
  templateUrl: './volt-list.component.html',
  styleUrls: ['./volt-list.component.css']
})
export class VoltListComponent implements OnInit {

  volts: Volt[];
  date: string|any = this.datePipe.transform(new Date(), "'yyyy-MM");
  
  constructor(private repository: RepositoryService,
    private datePipe: DatePipe,
    private authService: AuthenticationService) { }

  ngOnInit() {
    this.getVolts(this.date);
  }

  onChange(date:any){
    this.getVolts(date.target.value);
  }

  private getVolts(date: string){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('api/volt/list/'+agentId+'/'+date)
    .subscribe(res =>
      this.volts = res as Volt[]);
  }

}
