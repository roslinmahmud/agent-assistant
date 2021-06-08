import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PasswordConfirmationValidatorsService } from 'src/app/shared/custom-validators/password-confirmation-validators.service';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  public registerForm: FormGroup
  public errorMessage: string = '';
  public showError: boolean;

  constructor(private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorsService,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('')
    });
    this.registerForm.get('confirmPassword').setValidators([Validators.required,
      this.passConfValidator.validateConfirmPassword(this.registerForm.get('password'))]);
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName);
  }

  public registerUser = () => {
    this.showError = false;
    this.authService.registerUser('api/agent/registration', this.registerForm.value)
    .subscribe(_ => {
      this.router.navigate(["/authentication/login"]);
    },
    error => {
      console.log(error);
      this.errorMessage = error;
      this.showError = true;
    })
  }

}
