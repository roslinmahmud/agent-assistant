import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VoltFormComponent } from './volt-form/volt-form.component';
import { VoltListComponent } from './volt-list/volt-list.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    VoltFormComponent,
    VoltListComponent
  ],
  exports: [
    VoltFormComponent,
    VoltListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: 'form', component: VoltFormComponent},
      {path: 'list', component: VoltListComponent}
    ])
  ]
})
export class VoltModule { }
