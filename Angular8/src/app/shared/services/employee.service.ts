import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Employee } from '../../shared/models/employee.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import * as _ from 'lodash';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http:HttpClient) { }


form: FormGroup = new FormGroup({
  $key: new FormControl(null),
  employeeID: new FormControl(null),
  fullName: new FormControl('', Validators.required),
  email: new FormControl('', Validators.email),
  mobile: new FormControl('', [Validators.required, Validators.minLength(8)]),
  city: new FormControl(''),
  gender: new FormControl(1),
  departmentID: new FormControl(0),
  hireDate: new FormControl('', Validators.required),
  isPermanent: new FormControl(false)
});


initializeFormGroup(){
  this.form.setValue({
    $key: null,
    employeeID: null,
    fullName: '',
    email: '',
    mobile: '',
    city: '',
    gender: 1,
    departmentID: 0,
    hireDate: '',
    isPermanent: false
  });
}

getEmployeeList():Observable<Employee[]>{
  return this.http.get<Employee[]>(environment.apiURL+'/Employee');
}


addEmployee(){
  var body = {
    fullName: this.form.value.fullName,
    email: this.form.value.email,
    mobile: this.form.value.mobile,
    city: this.form.value.city,
    gender: this.form.value.gender,
    departmentID: this.form.value.departmentID,
    hireDate: this.form.value.hireDate,
    isPermanent: this.form.value.isPermanent
  };
  return this.http.post(environment.apiURL+'/Employee', body);
}

updateEmployee(employeeID){
  var body = {
    fullName: this.form.value.fullName,
    email: this.form.value.email,
    mobile: this.form.value.mobile,
    city: this.form.value.city, 
    gender: this.form.value.gender,
    departmentID: this.form.value.departmentID,
    hireDate: this.form.value.hireDate,
    isPermanent: this.form.value.isPermanent
  };
  return this.http.put(environment.apiURL+'/Employee/'+employeeID, body);
}


populateForm(employee) {

  this.form.setValue(
    {
      $key: null,
      employeeID: employee.employeeID,
      fullName: employee.fullName,
      email: employee.email,
      mobile: employee.mobile,
      city: employee.city,
      gender: employee.gender,
      departmentID: employee.departmentID,
      hireDate: employee.hireDate,
      isPermanent: employee.isPermanent
    },
    _.omit(employee, 'DepartmentName')
    );

}


deleteEmployee(employeeID):Observable<Employee>{
  return this.http.delete<Employee>(environment.apiURL+'/Employee/'+employeeID);
}

}
