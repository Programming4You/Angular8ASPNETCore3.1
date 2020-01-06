import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  decodedToken:any;
  isAdmin:any;

  constructor(private router:Router, private service:UserService) { }

  ngOnInit() {
    this.isAdmin = this.service.getUserRole();
  }

  onLogout(){
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

}
