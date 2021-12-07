import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { StatementFormComponent } from './statement-form/statement-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StatementCategoryComponent } from './statement-category/statement-category.component';



@NgModule({
  declarations: [StatementFormComponent, StatementCategoryComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: "", component: StatementFormComponent}
    ])
  ]
})
export class StatementModule { }
