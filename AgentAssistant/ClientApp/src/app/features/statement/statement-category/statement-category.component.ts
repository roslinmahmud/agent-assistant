import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from '../../../shared/services/repository.service';
import { AuthenticationService } from '../../authentication/services/authentication.service';

@Component({
  selector: 'app-statement-category',
  templateUrl: './statement-category.component.html',
  styleUrls: ['./statement-category.component.css']
})
export class StatementCategoryComponent implements OnInit {

  statementCategories: StatementCategory[];
  isSubmitted: boolean;

  statementCategoryForm = this.formBuilder.group({
    categoryName: ['', Validators.required],
    isIncome: [false, Validators.required]
  })

  @Output() reload: EventEmitter<any> = new EventEmitter();

  constructor(private formBuilder: FormBuilder,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getStatementCategories();
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if(!this.statementCategoryForm.invalid){
      this.createStatementCategory(this.statementCategoryForm.value);
    }
  }

  private getStatementCategories(){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('/api/statement/category/'+agentId)
    .subscribe(res =>
      this.statementCategories = res as StatementCategory[]);
  }

  private createStatementCategory(StatementCategory: StatementCategory){
    const apiUrl = "/api/statement/category";
    StatementCategory.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, StatementCategory)
      .subscribe(
        res => {
          this.statementCategories.push(res as StatementCategory);
          this.toastr.success('Submit successful', '');
          this.statementCategoryForm.reset();
          this.isSubmitted = false;
        },
        (error)=>{
          console.error(error);
        },
        () => {
          this.reload.emit();
        }
      );
  }

}
