import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  //from parent to child
  //@Input() UsersFromHomeComponent:any;
  //from child to parent
  @Output() CancelRegister = new EventEmitter()
  constructor(private accountServices: AccountService,private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  register() {
    this.accountServices.Register(this.model).subscribe({
      next: () => this.cancel(),
      error: error => this.toastr.error(error.error)
    })
  }
  cancel() {
    // console.log("cancelled");
    this.CancelRegister.emit(false);
  }
}
