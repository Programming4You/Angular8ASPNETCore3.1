import { Component, OnInit } from '@angular/core';
import { UserService } from '../../shared/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})

export class RegistrationComponent implements OnInit {

  constructor(private service: UserService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
  }

  onSubmit(){
    this.service.register().subscribe(
     (res:any) => {
       if(res['succeeded'] == true) {
        this.service.formModel.reset();
        this.toastr.success('New user created!','Registration successful');
       } 
       else {
        this.toastr.error('Username is already taken!','Registration failed');
       }
     },
     err => {
       console.log(err);
     }
    );
  }

}
