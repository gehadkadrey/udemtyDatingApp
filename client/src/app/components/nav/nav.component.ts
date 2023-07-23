import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'console';
import { ToastrService } from 'ngx-toastr';
import { Observable,of } from 'rxjs';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  currentUser$:Observable<User | null>=of(null);
  //LoggedIn = false;
  constructor(public accountService: AccountService,private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {

  }
 
  login() {
    this.accountService.login(this.model).subscribe(
      {
        next: _ => this.router.navigateByUrl('/members'),
        error: error => this.toastr.error(error.error)
      })
  }//login
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
