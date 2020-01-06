import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { OrderService } from '../shared/services/order.service';


@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  lineChart=[];
  ordersNo=[];
  gTotals=[];
  total=[];

  constructor(private service:OrderService) { }

  ngOnInit() {
    this.getOrders();
  }


  getOrders(){
    this.service.getOrdersChart().subscribe(res => {
      res.forEach((i) => this.ordersNo.push(i.orderNo)); 
      res.forEach((i) => this.gTotals.push(i.gTotal)); 
      this.total = this.gTotals;
      this.chartDiagram(); 
    });
  }


  chartDiagram(){
    // Line chart:
    this.lineChart = new Chart('lineChart', {
      type: 'line',
    data: {
    labels: this.ordersNo,
    datasets: [{
        label: 'Grand Total by Order',
        data: this.total,
        fill:false,
        lineTension:0.2,
        borderColor:"red",
        borderWidth: 1
    }]
    }, 
    options: {
    title:{
        text:"Line Chart",
        display:true
    },
    scales: {
        yAxes: [{
            ticks: {
                beginAtZero:true
            }
        }]
    }
    }
    });
  }

}
