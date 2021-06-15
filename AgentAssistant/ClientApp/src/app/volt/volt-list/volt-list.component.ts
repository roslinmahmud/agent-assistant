import { Component, OnInit } from '@angular/core';
import { Volt } from '../../interfaces/volt';
import { RepositoryService } from '../../repository.service';

import { registerLocaleData } from '@angular/common';
import  localeBn from '@angular/common/locales/bn';

registerLocaleData(localeBn, 'bn');

@Component({
  selector: 'app-volt-list',
  templateUrl: './volt-list.component.html',
  styleUrls: ['./volt-list.component.css']
})
export class VoltListComponent implements OnInit {

  volts: Volt[];

  
  constructor(private repository: RepositoryService) { }

  ngOnInit() {
    this.repository.get('api/volt').subscribe(res =>
      this.volts = res as Volt[]);
  }

}
