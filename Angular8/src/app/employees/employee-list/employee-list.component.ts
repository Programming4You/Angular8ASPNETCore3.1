import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { EmployeeService } from 'src/app/shared/services/employee.service';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { Employee } from 'src/app/shared/models/employee.model';
import { MatDialog, MatDialogConfig } from "@angular/material";
import { EmployeeComponent } from '../employee/employee.component';
import { ToastrService } from 'ngx-toastr';
import { DialogService } from '../../shared/services/dialog.service';



@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  constructor(private service: EmployeeService, private dialog: MatDialog, private toastr: ToastrService, private dialogService: DialogService, 
    private changeDetectorRefs: ChangeDetectorRef) { }


  listData: MatTableDataSource<Employee>;
  displayedColumns: string[] = ['FullName','Email','Mobile','City', 'DepartmentName','Actions'];
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild('ELEMENT', {static: true}) table: any;
  searchKey: string;
  

  ngOnInit() {
    this.refresh();
  }

  
  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  onCreate() {
    this.service.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(EmployeeComponent, dialogConfig)
    .afterClosed().subscribe(res => {
      this.refresh();
    });
  }


  onEdit(employee) {
    this.service.populateForm(employee);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    this.dialog.open(EmployeeComponent, dialogConfig)
    .afterClosed().subscribe(res => {
      this.refresh();
    });
  }
 
  onDelete(employeeID){
    this.dialogService.openConfirmDialog('Are you sure to delete this record?')
    .afterClosed().subscribe(res => {
     if(res){
      this.service.deleteEmployee(employeeID).subscribe( res => {
      this.service.getEmployeeList().subscribe(res => {
        this.listData = new MatTableDataSource(res);
        this.listData.sort = this.sort;         
        this.listData.paginator = this.paginator;
      });
      this.toastr.success('Deleted successfully!');
    }, error => {
      console.log(error);
    });
    }
    });
  }


  refresh() {
    this.service.getEmployeeList().subscribe(res => {
      this.listData = new MatTableDataSource(res);
      this.listData.sort = this.sort;         
      this.listData.paginator = this.paginator;
      this.changeDetectorRefs.detectChanges();
    });
  }


}