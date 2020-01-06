import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material";
import { OrderItem } from '../../shared/models/order-item.model';
import { ItemService } from 'src/app/shared/services/item.service';
import { Item } from '../../shared/models/item.model';
import { NgForm } from '@angular/forms';
import { OrderService } from 'src/app/shared/services/order.service';


@Component({
  selector: 'app-order-items',
  templateUrl: './order-items.component.html',
  styleUrls: ['./order-items.component.css']
})
export class OrderItemsComponent implements OnInit {
  formData:OrderItem;
  itemList:Item[];
  isValid:boolean = true;
  orderItem;
  selectedItem;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data,
    public dialogRef:MatDialogRef<OrderItemsComponent>,
    private itemService:ItemService,
    private orderService:OrderService
    ) { }

  ngOnInit() {
    this.orderItem = Object.assign({}, this.orderService.orderItems[this.data.orderItemIndex]);
    this.itemService.getItemList().subscribe(res => {
      this.itemList = res
    })

    if(this.data.orderItemIndex == null)
    this.formData = {
      OrderItemID: null,
      OrderID: this.data.OrderID,
      ItemID: 0,
      Quantity: 0,
      ItemName:'',
      Price:0,
      total:0
    }
     else
      this.PopulateOrderItemForm(this.orderItem);   
  }

  PopulateOrderItemForm(orderItem){
    this.formData = {
      OrderItemID: orderItem.orderItemID,
      OrderID: orderItem.orderID,
      ItemID: orderItem.itemID,
      Quantity: orderItem.quantity,
      ItemName: orderItem.itemName,
      Price: orderItem.price,
      total: orderItem.total
    }   
  }

  updatePrice(ctrl){
    if(ctrl.selectedIndex==0){
      this.formData.Price=0;
      this.formData.ItemName='';
    } else{
      this.formData.Price=this.itemList[ctrl.selectedIndex-1].price;
      this.formData.ItemName=this.itemList[ctrl.selectedIndex-1].name;
    }
    this.updateTotal();
  }

  updateTotal(){
    this.formData.total = parseFloat((this.formData.Quantity * this.formData.Price).toFixed(2));
  }

  onSubmit(form:NgForm){
    if(this.validateForm(form.value)){
      if(this.data.orderItemIndex == null)
        this.orderService.orderItems.push(form.value);
      else
        this.orderService.orderItems[this.data.orderItemIndex] = form.value;
        this.dialogRef.close();
   }
  }

  validateForm(formData:OrderItem){
    this.isValid = true;

    if(formData.ItemID==0)
      this.isValid = false;
    else if (formData.Quantity==0)
      this.isValid = false;

     return this.isValid;
  }

}
