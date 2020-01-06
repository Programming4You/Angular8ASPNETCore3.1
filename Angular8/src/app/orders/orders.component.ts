import { Component, OnInit } from '@angular/core';
import { OrderService } from '../shared/services/order.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/services/user.service';
import { Order } from '../shared/models/order.model';


@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orderList;
  userDetails;
  order:Order;
  pageOfItems: Array<any>;
  items=[];

  constructor(private service:OrderService, private router:Router, private toastr:ToastrService, private userService:UserService) { }

  ngOnInit() {
    this.refreshList();
 
    this.userService.getUserProfile().subscribe(
      res => {
        this.userDetails = res;
      },
      err => {
        console.log(err);
      }
    );
  }
 
  refreshList(){
    this.service.getOrderList().subscribe(res => {
      this.orderList = res
      this.items=this.orderList;
    });
  }

  openForEdit(orderID:number){
    this.router.navigate(['home/order/'+orderID]);
  }

  onOrderDelete(id:number){
    if(confirm("Are you sure to delete this record?")){
      this.service.deleteOrder(id).subscribe(res => {
        this.refreshList();
        this.toastr.warning("Deleted Successfully","Company App");
        });
    }
  }

  onChangePage(pageOfItems: Array<any>) {
    this.orderList = pageOfItems;
}

}
