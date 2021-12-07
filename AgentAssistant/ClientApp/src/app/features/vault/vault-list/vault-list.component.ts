import { Component, OnInit } from '@angular/core';
import { Vault } from '../../../interfaces/vault';
import { RepositoryService } from '../../../shared/services/repository.service';

import { DatePipe, registerLocaleData } from '@angular/common';
import  localeBn from '@angular/common/locales/bn';
import { AuthenticationService } from '../../authentication/services/authentication.service';

registerLocaleData(localeBn, 'bn');

@Component({
  selector: 'app-vault-list',
  templateUrl: './vault-list.component.html',
  styleUrls: ['./vault-list.component.css']
})
export class VaultListComponent implements OnInit {

  vaults: Vault[];
  date: string|any = this.datePipe.transform(new Date(), "'yyyy-MM");
  
  constructor(private repository: RepositoryService,
    private datePipe: DatePipe,
    private authService: AuthenticationService) { }

  ngOnInit() {
    this.getVaults(this.date);
  }

  onChange(date:any){
    this.getVaults(date.target.value);
  }

  private getVaults(date: string){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('/api/vault/list/'+agentId+'/'+date)
    .subscribe(res =>
      this.vaults = res as Vault[]);
  }

}
