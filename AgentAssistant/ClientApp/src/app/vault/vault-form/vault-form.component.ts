import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Vault } from '../../interfaces/vault';
import { RepositoryService } from '../../repository.service';
import { AuthenticationService } from '../../shared/services/authentication.service';


@Component({
  selector: 'app-vault-form',
  templateUrl: './vault-form.component.html',
  styleUrls: ['./vault-form.component.css']
})
export class VaultFormComponent implements OnInit {

  vault: Vault;
  isSubmitted: boolean;
  date: string = this.datePipe.transform(new Date(), "yyyy-MM-dd");
  
  vaultForm = this.fromBuilder.group({
    date: [this.date, Validators.required],
    openingLiquidMoney: ['0', Validators.required],
    openingCashMoney: ['0', Validators.required],
    closingLiquidMoney:['0'],
    closingCashMoney:['0']
  })

  constructor(private fromBuilder: FormBuilder,
    private http: HttpClient,
    private datePipe: DatePipe,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getVault(this.date);
  }

  onChange(){
    this.getVault(this.vaultForm.get('date').value);
  }

  private getVault(date:string) {
    const apiUrl = "api/vault/" + this.authService.getAgentId() + "/"+this.datePipe.transform(date, "yyyy-MM-dd");

    this.repository.get(apiUrl)
      .subscribe(res => {
        if(res == null){
          this.vaultForm.reset({date: date} as Vault);
          this.isSubmitted = false;
          if(date != this.date)
            this.toastr.warning("Date: "+date, 'No data found')
          return;
        }
          
        this.vault = res as Vault;
        this.vault.date = this.datePipe.transform(this.vault.date, "yyyy-MM-dd")
        this.vaultForm.patchValue(this.vault);
        this.isSubmitted = true;
      },
      (error)=>{
        console.warn(error);
      });
  }

  onSubmit() {
    if(this.isSubmitted){
      this.updateVault(Object.assign(this.vault, this.vaultForm.value));
    }
    else{
      this.createVault(this.vaultForm.value);
    }
  }

  private createVault(vault: Vault){
    const apiUrl = "api/vault";
    vault.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, vault)
      .subscribe(res => {
        this.vault = res as Vault;
        this.isSubmitted = true;
        this.toastr.success('Submit successful', '');
      },
      (error)=>{
        console.error(error);
      });
  }

  private updateVault(vault: Vault){
    const apiUrl = "api/vault/";
    vault.agentId = this.authService.getAgentId();

    this.repository.update(apiUrl, vault)
      .subscribe(res => {
        this.toastr.success('Update successful', 'Success');
      },
      (error => {
        this.toastr.error("Update failed", error);
      })
    );
  }
}
