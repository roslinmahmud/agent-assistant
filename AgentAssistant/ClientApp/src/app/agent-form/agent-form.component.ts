import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-agent-form',
  templateUrl: './agent-form.component.html',
  styleUrls: ['./agent-form.component.css']
})
export class AgentFormComponent implements OnInit {

  agentForm = new FormGroup({
    name: new FormControl('Roslin'),
    phoneNumber: new FormControl('01985200855')
  })

  constructor() { }

  ngOnInit() {
  }

  onSubmit(){
    console.warn(this.agentForm.value.name);
  }


}
