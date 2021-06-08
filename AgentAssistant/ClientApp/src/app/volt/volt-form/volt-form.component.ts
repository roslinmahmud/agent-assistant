import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Volt } from '../../interfaces/volt';
import { RepositoryService } from '../../repository.service';
import { AuthenticationService } from '../../shared/services/authentication.service';


@Component({
  selector: 'app-volt-form',
  templateUrl: './volt-form.component.html',
  styleUrls: ['./volt-form.component.css']
})
export class VoltFormComponent implements OnInit {

  voltForm = this.fromBuilder.group({
    date: ['', Validators.required],
    openingLiquidMoney: ['', Validators.required],
    openingCashMoney: ['', Validators.required],
    closingLiquidMoney:[''],
    closingCashMoney:['']
  })

  isSubmitted: boolean = false;

  constructor(private fromBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private datePipe: DatePipe,
    private repository: RepositoryService,
    private authService: AuthenticationService  ) { }

  ngOnInit() {
    this.loadVolt();
  }

  onSubmit() {
    this.createVolt(this.voltForm.value);
  }

  private createVolt(volt: Volt){
    const apiUrl = "api/volt";
    volt.agentId = this.authService.getAgentId();
    this.repository.create(apiUrl, volt)
      .subscribe(res => {
        console.log("Volt submitted:", res);
        this.isSubmitted = true;
      });
  }

  private loadVolt() {
    const apiUrl = "api/volt/" + this.authService.getAgentId();

    this.repository.get(apiUrl)
      .subscribe(res => {
        const volt = res as Volt;
        this.voltForm.patchValue(volt);
        this.isSubmitted = true;
      });
  }
}
