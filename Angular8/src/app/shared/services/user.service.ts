import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import * as jwt_decode from 'jwt-decode';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  decodeToken:any;
  isAdminRole:any;

  constructor(private fb:FormBuilder, private http:HttpClient) { 
  }

formModel = this.fb.group({
   UserName: ['', Validators.required],
   Email: ['', Validators.email],
   FullName: [''],
   Passwords: this.fb.group({
    Password: ['', [Validators.required, Validators.minLength(4)]],
    ConfirmPassword: ['', Validators.required]
   }, {validator: this.comparePasswords})
});

comparePasswords(fb:FormGroup){
  let confirmPasswordCtrl = fb.get('ConfirmPassword');
  if(confirmPasswordCtrl.errors == null || 'passwordMismatch' in confirmPasswordCtrl.errors){
    if(fb.get('Password').value != confirmPasswordCtrl.value)
       confirmPasswordCtrl.setErrors({ passwordMismatch: true });
    else
       confirmPasswordCtrl.setErrors(null);
  }
}

register(){
  var body = {
    UserName: this.formModel.value.UserName,
    Email: this.formModel.value.Email,
    FullName: this.formModel.value.FullName,
    Password: this.formModel.value.Passwords.Password
  };
  return this.http.post(environment.apiURL+'/ApplicationUser/Register', body);
}

login(formData){
  return this.http.post(environment.apiURL+'/ApplicationUser/Login', formData);
}

getUserProfile(){
  return this.http.get(environment.apiURL+'/UserProfile');  //interceptor provides tokenHeader
}

roleMatch(allowedRoles): boolean {
  var isMatch = false;
  var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
  var userRole = payLoad.role;
  allowedRoles.forEach(element => {
    if (userRole == element) {
      isMatch = true;
      return false;
    }
  });
  return isMatch;
}

getUserRole(): string {
  var token = localStorage.getItem('token');
  this.decodeToken = jwt_decode(token);  
  return this.decodeToken['role'];
}

getIsAdminRole(){
  let jwt = localStorage.getItem('token');
  let jwtData = jwt.split('.')[1]
  let decodedJwtJsonData = window.atob(jwtData)
  let decodedJwtData = JSON.parse(decodedJwtJsonData)
  
  return decodedJwtData.role;
} 

loggedIn(){
  return !!localStorage.getItem('token');  //returns true or false
}

}
