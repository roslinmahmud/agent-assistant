import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Statement, StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from 'src/app/repository.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-statement-form',
  templateUrl: './statement-form.component.html',
  styleUrls: ['./statement-form.component.css']
})
export class StatementFormComponent implements OnInit {

  date: String|any = this.datePipe.transform(new Date(), "'yyyy-MM");
  statements: Statement[] = [];
  statementCategories: StatementCategory[] = [];
  statementCategoryMap = new Map<number, StatementCategory>();
  netIncome:number = 0;

  statementForm = this.formBuilder.group({
    date: [this.datePipe.transform(new Date(), "yyyy-MM-dd"), Validators.required],
    amount: ['', Validators.required],
    categoryId: ['', Validators.required],
    description: ['', Validators.required]
  })

  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private datePipe: DatePipe,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getStatementCategories();
    this.getStatements(this.date);
  }

  onSubmit(): void{
    this.statementForm.markAsPending();
    if(!this.statementForm.invalid){
      this.createStatement(this.statementForm.value);
    }
  }

  onChange(date:any){
    this.getStatements(date.target.value);
  }

  private getStatements(date: string){
    const apiUrl = 'api/statement/';
    let agentId:string = this.authService.getAgentId();

    this.repository.get(apiUrl+agentId+'/'+date)
    .subscribe(
      res =>{ this.statements = res as Statement[]},
      (error) => {},
      () => {
        this.statements.map(st => {
          if(this.statementCategoryMap.get(st.categoryId).isIncome){
            this.netIncome+=st.amount;
          }
          else{
            this.netIncome-=st.amount;
          }
        })
      }
    );
  }

  private getStatementCategories(){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('api/statement/category/'+agentId)
    .subscribe(
      (res: StatementCategory[]) => {this.statementCategories = res;},
      (error) => {},
      () => {this.statementCategories.map( sc => this.statementCategoryMap.set(sc.id, sc));}
    );
  }

  private createStatement(Statement: Statement){
    const apiUrl = "api/statement";
    Statement.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, Statement)
      .subscribe(
        res => {
          this.statements.push(res as Statement);
          this.toastr.success('Submit successful', '');
        },
        (error)=>{
          console.error(error);
        }
      );
  }

}
