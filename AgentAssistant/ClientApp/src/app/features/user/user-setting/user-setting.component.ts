import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/features/authentication/services/authentication.service';
import { StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from 'src/app/shared/services/repository.service';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.css']
})
export class UserSettingComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    
  }

  
  
  

}
