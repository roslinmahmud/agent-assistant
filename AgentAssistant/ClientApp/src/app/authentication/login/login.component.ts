import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private returnUrl: string;

  loginForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  })

  constructor(private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  public validateControl = (controlName: string) => {
    return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName);
  }

  onSubmit(): void{
    if(this.loginForm.valid){
      this.loginUser();
    }
    else{
      this.toastr.warning("Please enter valid credential","Invalid Input");
    }
  }

  public loginUser = () => {
 
    this.authService.loginUser('api/user/login', this.loginForm.value)
    .subscribe(res => {
      localStorage.setItem("token", res.token);
      this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
      this.router.navigate([this.returnUrl]);
    },
    (error) => {
      this.toastr.error(error, "Signin failed");
    });
  }

}
