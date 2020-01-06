import { Component, OnInit, Input } from '@angular/core';
import { EmployeeService } from '../../shared/services/employee.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MatDialogRef } from '@angular/material';
import { DepartmentService } from '../../shared/services/department.service';
import { Department } from '../../shared/models/department.model';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  departmentList: Department[];
  selectedOption;

  constructor(private service: EmployeeService, private departmentService: DepartmentService, private toastr: ToastrService, private router: Router, 
    public dialogRef: MatDialogRef<EmployeeComponent>) { }

 
  ngOnInit() { 
    this.departmentService.getDepartmentList().subscribe(
      res => {
        this.departmentList = res;
      },
      err => {
        console.log(err);
      }
    );
    this.selectedOption = this.service.form.get('departmentID').value;
  }

  onClear() {
    this.service.form.reset();
    this.service.initializeFormGroup();
  }


  onSubmit() {
    if (!this.service.form.get('employeeID').value) {
    this.service.addEmployee().subscribe(res => {
      this.service.form.reset();
      this.toastr.success('New employee created!','Employee added successful');
      this.onClose();
      this.router.navigate(['/home/employee']);
    });
  } else {
    this.service.updateEmployee(this.service.form.get('employeeID').value).subscribe(res => {
      this.service.form.reset();
      this.toastr.success('Employee updated!','Employee updated successful');
      this.onClose();
      this.router.navigate(['/home/employee']);
    });
  }
  this.router.navigate(['/home/employee']);
  }


  onClose() {
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.dialogRef.close();
  }


}
