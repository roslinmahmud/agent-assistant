import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Volt } from '../interfaces/volt';
import { RepositoryService } from '../repository.service';


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
    closingLiquidMoney:['', Validators.required],
    closingCashMoney:['', Validators.required]
  })

  constructor(private fromBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private datePipe: DatePipe,
    private repository: RepositoryService) { }

  ngOnInit() {
  }

  onSubmit(){
    this.createVolt(this.voltForm.value);
  }

  private createVolt(volt: Volt){
    const apiUrl = "api/volt";
    this.repository.create(apiUrl, volt)
    .subscribe(res => {
      console.log("log:", res);
    })
  }

}
