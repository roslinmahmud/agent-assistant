import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/features/authentication/services/authentication.service';
import { StatementCategory } from 'src/app/interfaces/statement';
import { RepositoryService } from 'src/app/shared/services/repository.service';
import { Agent } from '../models/agent';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.css']
})
export class UserSettingComponent implements OnInit {

  public agentForm: FormGroup;
  public isSubmitted: boolean;
  public agent: Agent;

  constructor(private formBuilder: FormBuilder,
    private repository: RepositoryService,
    private authService: AuthenticationService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAgent();
  }

  initForm(agent: Agent) {
    this.agentForm = this.formBuilder.group({
      id: [this.authService.getAgentId()],
      name: [this.agent.name],
      email: [this.agent.email],
      password: [this.agent.password]
    })
  }

  public validateControl = (controlName: string) => {
    return this.agentForm.controls[controlName].valid;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.agentForm.controls[controlName].hasError(errorName);
  }

  onSubmit(): void {
    this.isSubmitted = true;
    if (this.agentForm.valid) {
      this.updateAgent();
    }
    else {
      this.toastr.warning("Please enter valid information", "Invalid Input");
    }
  }

  public updateAgent = () => {
    this.authService.registerUser('api/agent/', this.agentForm.value)
      .subscribe(
        res => {
          this.isSubmitted = false;
        },
        error => {
          this.toastr.error(error, "Agent authentication failed");
        }
      )
  }

  private getAgent() {
    let agentId: string = this.authService.getAgentId();
    this.repository.get('/api/agent/' + agentId)
      .subscribe(res => {
        this.agent = res as Agent
        this.initForm(this.agent);
      });
  }
}
