import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VaultFormComponent } from './vault-form/vault-form.component';
import { VaultListComponent } from './vault-list/vault-list.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    VaultFormComponent, VaultListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: '', component: VaultFormComponent}
    ])
  ]
})
export class VaultModule { }
