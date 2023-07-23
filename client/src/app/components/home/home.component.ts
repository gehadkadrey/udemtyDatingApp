import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users:any;
  constructor(private httpClient:HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
  }//registerToggle
  getUsers() {
    this.httpClient.get("https://localhost:7197/api/user").subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("complete")
    });
  }//getusers
  cancelRegisterMode(event:boolean)
  {
    //event is false send from child register
    this.registerMode=event;
  }
}
