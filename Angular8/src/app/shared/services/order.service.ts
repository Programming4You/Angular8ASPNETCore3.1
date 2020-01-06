import { Injectable } from '@angular/core';
import { Order } from '../../shared/models/order.model';
import { OrderItem } from '../../shared/models/order-item.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { OrderChart } from '../models/order-chart';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  formData:Order;
  orderItems:OrderItem[];

  constructor(private http:HttpClient) { }

  saveOrUpdateOrder(){

    var body = {
       ...this.formData,
       orderItems: this.orderItems
     }

     return this.http.post(environment.apiURL+'/Order', body);
   }


  getOrderList():Observable<Order[]>{
    return this.http.get<Order[]>(environment.apiURL+'/Order');
  }

  getOrderByID(id:number):Observable<any>{
    return this.http.get<Order[]>(environment.apiURL+'/Order/'+id);
  }

  deleteOrder(id:number):Observable<Order>{
    return this.http.delete<Order>(environment.apiURL+'/Order/'+id);
  }



  populateForm(order){
    this.formData = {
      OrderID: order.orderID,
      OrderNo: order.orderNo,
      CustomerID: order.customerID,
      PMethod: order.pMethod,
      GTotal: order.gTotal,
      DeletedOrderItemIDs: order.deletedOrderItemIDs
    };
  }


  getOrdersChart():Observable<OrderChart[]>{
    return this.http.get<OrderChart[]>(environment.apiURL+'/Order');
  }

}
