import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  public isUserAuthenticated: boolean;

  constructor(private authService: AuthenticationService,
    private router: Router,
    private toastrService: ToastrService){}

  ngOnInit(): void {
    this.authService.authChanged
    .subscribe(res => {
      this.isUserAuthenticated = res;
    })
    this.toastrService.success('success', 'upon success');
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(["/"]);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
