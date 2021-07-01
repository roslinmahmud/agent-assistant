import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from 'src/app/repository.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.css']
})
export class UserSettingComponent implements OnInit {

  statementCategories: StatementCategory[];

  statementCategoryForm = this.formBuilder.group({
    categoryName: ['', Validators.required],
    isIncome: [false, Validators.required]
  })

  constructor(private formBuilder: FormBuilder,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getStatementCategories();
  }

  onSubmit(): void {
    this.statementCategoryForm.markAsPending();
    if(!this.statementCategoryForm.invalid){
      this.createStatementCategory(this.statementCategoryForm.value);
    }
  }
  
  private createStatementCategory(StatementCategory: StatementCategory){
    const apiUrl = "api/statement/category";
    StatementCategory.agentId = this.authService.getAgentId();

    this.repository.create(apiUrl, StatementCategory)
      .subscribe(
        res => {
          this.statementCategories.push(res as StatementCategory);
          this.toastr.success('Submit successful', '');
        },
        (error)=>{
          console.error(error);
        }
      );
  }

  private getStatementCategories(){
    let agentId:string = this.authService.getAgentId();
    this.repository.get('api/statement/category/'+agentId)
    .subscribe(res =>
      this.statementCategories = res as StatementCategory[]);
  }
}
