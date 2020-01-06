import { Component } from '@angular/core';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { UserService } from './shared/services/user.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Angular8';
  faCoffee = faCoffee;
  isAdmin:any;

  constructor(private router:Router, private service:UserService){}

   ngOnInit() {
  }

}


