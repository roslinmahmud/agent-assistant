import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserSettingComponent } from './user-setting/user-setting.component';



@NgModule({
  declarations: [UserSettingComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {path: 'setting', component: UserSettingComponent}
    ])
  ]
})
export class UserModule { }
