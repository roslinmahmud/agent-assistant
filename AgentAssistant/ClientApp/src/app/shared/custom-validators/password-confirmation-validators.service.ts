import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PasswordConfirmationValidatorsService {

  constructor() { }

  public validateConfirmPassword = (passwordControl: AbstractControl): ValidatorFn => {
    return (confirmPasswordControl: AbstractControl): {[key:string]: boolean} | null => {
      const confirmPasswordValue = confirmPasswordControl.value;
      const passwordValue = passwordControl.value;

      if(confirmPasswordValue == ''){
        return;
      }

      if(confirmPasswordValue != passwordValue){
        return {mustMatch: true}
      }

      return null;
    }
  }
}
