import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Volt } from '../../interfaces/volt';
import { RepositoryService } from '../../repository.service';
import { AuthenticationService } from '../../shared/services/authentication.service';


@Component({
  selector: 'app-volt-form',
  templateUrl: './volt-form.component.html',
  styleUrls: ['./volt-form.component.css']
})
export class VoltFormComponent implements OnInit {

  volt: Volt;
  isSubmitted: boolean;
  date: string = this.datePipe.transform(new Date(), "yyyy-MM-dd");;

  voltForm = this.fromBuilder.group({
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
    this.getVolt(this.date);
  }

  onChange(){
    this.getVolt(this.voltForm.get('date').value);
  }

  private getVolt(date:string|Date) {
    const apiUrl = "api/volt/" + this.authService.getAgentId() + "/"+this.datePipe.transform(date, "yyyy-MM-dd");

    this.repository.get(apiUrl)
      .subscribe(res => {
        if(res == null)
          return;
        this.volt = res as Volt;
        this.volt.date = this.datePipe.transform(this.volt.date, "yyyy-MM-dd")
        this.voltForm.patchValue(this.volt);
        this.isSubmitted = true;
      },
      (error)=>{
        console.warn(error);
      });
  }

  onSubmit() {
    if(this.isSubmitted){
      this.updateVolt(Object.assign(this.volt, this.voltForm.value));
    }
    else{
      this.createVolt(this.voltForm.value);
    }
  }

  private createVolt(volt: Volt){
    const apiUrl = "api/volt";
    volt.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, volt)
      .subscribe(res => {
        this.volt = res as Volt;
        this.isSubmitted = true;
        this.toastr.success('Submit successful', '');
      },
      (error)=>{
        console.error(error);
      });
  }

  private updateVolt(volt: Volt){
    const apiUrl = "api/volt/";
    volt.agentId = this.authService.getAgentId();

    this.repository.update(apiUrl, volt)
      .subscribe(res => {
        this.toastr.success('Update successful', 'Success');
        console.log(res, "updated!");
      });
  }
}
