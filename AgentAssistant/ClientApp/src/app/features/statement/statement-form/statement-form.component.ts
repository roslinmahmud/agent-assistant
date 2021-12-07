import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { ToastrService } from 'ngx-toastr';
import { Statement, StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from '../../../shared/services/repository.service';
import { AuthenticationService } from '../../authentication/services/authentication.service';


@Component({
  selector: 'app-statement-form',
  templateUrl: './statement-form.component.html',
  styleUrls: ['./statement-form.component.css']
})
export class StatementFormComponent implements OnInit {

  isSubmitted: boolean;
  date: String|any = this.datePipe.transform(new Date(), "yyyy-MM");
  statements: Statement[] = [];
  statementCategories: StatementCategory[] = [];
  statementCategoryMap = new Map<number, StatementCategory>();
  netIncome:number = 0;

  statementForm = this.formBuilder.group({
    date: ['', Validators.required],
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
  }

  public openPDF():void {
    let DATA = document.getElementById('statementData');
      
    html2canvas(DATA).then(canvas => {
        
        let fileWidth = 208;
        let fileHeight = canvas.height * fileWidth / canvas.width;
        
        const FILEURI = canvas.toDataURL('image/png')
        let PDF = new jsPDF('p', 'mm', 'a4');
        let position = 0;
        PDF.addImage(FILEURI, 'PNG', 0, position, fileWidth, fileHeight)
        
        PDF.save('angular-demo.pdf');
    });     
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if(this.statementForm.valid){
      this.createStatement(this.statementForm.value);
    }
  }

  onChange(date:any){
    this.date = date.target.value;
    this.getStatements(this.date);
  }

  getStatementCategories(){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('/api/statement/category/'+agentId)
    .subscribe(
      (res: StatementCategory[]) => {this.statementCategories = res;},
      (error) => {},
      () => {
        this.statementCategories.map( sc => this.statementCategoryMap.set(sc.id, sc));
        this.getStatements(this.date);
      }
    );
  }

  private getStatements(date: string){
    const apiUrl = '/api/statement/';
    let agentId:string = this.authService.getAgentId();

    this.repository.get(apiUrl+agentId+'/'+date)
    .subscribe(
      res =>{ this.statements = res as Statement[]},
      (error) => {},
      () => {
        this.netIncome = 0;
        this.statements.map(st => this.statementCategoryMap.get(st.categoryId).isIncome ? this.netIncome+=st.amount : this.netIncome-=st.amount )
      }
    );
  }

  private createStatement(Statement: Statement){
    const apiUrl = "/api/statement";
    Statement.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, Statement)
      .subscribe(
        (res: Statement) => {
          let date = this.datePipe.transform(res.date, "yyyy-MM");
          if(date != this.date){
              this.date = date;
              this.getStatements(this.date);
          }
          else{
            this.statements.push(res);
            this.statementCategoryMap.get(res.categoryId).isIncome ? this.netIncome+=res.amount : this.netIncome-=res.amount;
          }
          this.statementForm.reset();
          this.toastr.success('Statement created', 'Submit successful');
          this.isSubmitted = false;
        },
        (error)=>{},
        ()=>{}
      );
  }

}
