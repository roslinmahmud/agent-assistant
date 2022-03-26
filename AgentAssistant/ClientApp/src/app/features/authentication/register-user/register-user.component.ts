import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PasswordConfirmationValidatorsService } from 'src/app/shared/custom-validators/password-confirmation-validators.service';
import { AuthenticationService } from '../services/authentication.service';


@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {
  public registerForm: FormGroup;
  public isSubmitted: boolean;


  constructor(private authService: AuthenticationService,
    private passConfValidator: PasswordConfirmationValidatorsService,
    private router: Router,
    private toastr: ToastrService) { }

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
    return this.registerForm.controls[controlName].valid;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName);
  }

  onSubmit(): void{
    this.isSubmitted = true;
    if(this.registerForm.valid){
      this.registerUser();
    }
    else{
      this.toastr.warning("Please enter valid information","Invalid Input");
    }
  }

  public registerUser = () => {
    this.authService.registerUser('/api/user/register', this.registerForm.value)
    .subscribe(res => {
      if (res.isSuccessfulRegistration) {
        this.isSubmitted = false;
        this.router.navigate(["/authentication/login"], {queryParams:{userId: res.userId} });
      }
      else{
        console.error(res.errros);
      }  
    },
    error => {
      this.toastr.error(error, "Registration failed");
    })
  }

  

}
