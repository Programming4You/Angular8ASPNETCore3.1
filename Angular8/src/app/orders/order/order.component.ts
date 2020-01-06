import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../shared/services/order.service';
import { NgForm } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { OrderItemsComponent } from '../order-items/order-items.component';
import { CustomerService } from '../../shared/services/customer.service';
import { Customer } from '../../shared/models/customer.model';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  customerList:Customer[];
  isValid:boolean = true;

  constructor(private service: OrderService, 
    private customerService: CustomerService, 
    private dialog:MatDialog, 
    private toastr:ToastrService, 
    private router:Router,
    private currentRoute:ActivatedRoute) { }

  ngOnInit() {
    let orderID = this.currentRoute.snapshot.paramMap.get('id');

    if(orderID == null)
      this.resetForm();
     else{
       this.service.getOrderByID(parseInt(orderID)).subscribe(res => {
         this.service.populateForm(res.order);
         this.service.orderItems = res.orderDetails;
       });
     }

    this.customerService.getCustomerList().subscribe(res => {
      this.customerList = res
    });

  }

  resetForm(form?:NgForm){
    if (form!=null)
    form.resetForm();
    this.service.formData = {
      OrderID: null,
      OrderNo: Math.floor((100000+Math.random()*900000)).toString(),
      CustomerID: 0,
      PMethod: '',
      GTotal: 0,
      DeletedOrderItemIDs: ''
    };
    this.service.orderItems=[];
  }

  AddOrEditOrderItem(orderItemIndex,OrderID){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    dialogConfig.disableClose = true;
    dialogConfig.width="50%";
    dialogConfig.data = {orderItemIndex,OrderID};
    this.dialog.open(OrderItemsComponent,dialogConfig).afterClosed().subscribe(res => {
      this.updateGrandTotal();
    });
  }

  OnDeleteOrderItem(orderItemID:number, i:number){
    if(orderItemID != null) 
      this.service.formData.DeletedOrderItemIDs += orderItemID + ",";

    this.service.orderItems.splice(i,1);
    this.updateGrandTotal();
  }

  updateGrandTotal(){
   this.service.formData.GTotal = this.service.orderItems.reduce((prev,curr)=>{    
        return prev+curr.total;
     },0)

    this.service.formData.GTotal = parseFloat(this.service.formData.GTotal.toFixed(2));
  }

  validateForm(){
    this.isValid = true;
    if(this.service.formData.CustomerID == 0)
      this.isValid = false;
    else if (this.service.orderItems.length == 0)
      this.isValid = false;

    return this.isValid;
  }

  onSubmit(form:NgForm) {
  if(this.validateForm())
  {
    this.service.saveOrUpdateOrder().subscribe(res => {
      this.resetForm();
      this.toastr.success('Submitted Sucessfully', 'Company App');
      this.router.navigate(['/home/orders']);
    })
  }
}

}
