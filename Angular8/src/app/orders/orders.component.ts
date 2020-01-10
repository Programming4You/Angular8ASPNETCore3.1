import { Component, OnInit } from '@angular/core';
import { OrderService } from '../shared/services/order.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/services/user.service';
import { Order } from '../shared/models/order.model';
import 'jspdf-autotable';
import * as jsPDF from 'jspdf';


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

DownloadReport(){  
  let row : any[] = []
  let rowD : any[] = []
  let col=['OrderNo','Customer','Payment Method','Grand Total'];
  let title = "Order Report" 
  for(let a=0;a<this.items.length;a++){
    row.push(this.items[a].orderNo)
    row.push(this.items[a].customerName)
    row.push(this.items[a].pMethod == '1' ? 'Cash' : 'Card')
    row.push(this.items[a].gTotal)
    rowD.push(row);
    row =[];
  }
  this.getReport(col , rowD , title );
}

getReport(col: any[], rowD: any[], title: any) {
      const totalPagesExp = "{total_pages_count_string}";        
      let pdf = new jsPDF('l', 'pt', 'legal');
      pdf.setTextColor(51, 156, 255);
      pdf.text("Company: MyCompany", 50, 55); //x-axis and y-axis
      pdf.text("" + title, 435,50);  
      pdf.setLineWidth(1.5);
      var pageContent = function (data) {
          var str = "Page " + data.pageCount;
          if (typeof pdf.putTotalPages === 'function') {
              str = str + " of " + totalPagesExp;
          }
          pdf.setFontSize(10);
          var pageHeight = pdf.internal.pageSize.height || pdf.internal.pageSize.getHeight();
          pdf.text(str, data.settings.margin.left, pageHeight - 10); 
      };
      pdf.autoTable(col, rowD,
          {
              addPageContent: pageContent,
              margin: { top: 70 },
          });

      if (typeof pdf.putTotalPages === 'function') {
          pdf.putTotalPages(totalPagesExp);
      }

      pdf.save(title + '.pdf');
}

}
