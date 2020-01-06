import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import * as Material from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OrdersComponent } from './orders/orders.component';
import { OrderComponent } from './orders/order/order.component';
import { OrderItemsComponent } from './orders/order-items/order-items.component';
import { OrderService } from './shared/services/order.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserService } from './shared/services/user.service';
import { LoginComponent } from './user/login/login.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ForbiddenComponent } from './auth/forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { EmployeeComponent } from './employees/employee/employee.component';
import { EmployeeService } from './shared/services/employee.service';
import { EmployeeListComponent } from './employees/employee-list/employee-list.component';
import { MatConfirmDialogComponent } from './mat-confirm-dialog/mat-confirm-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableExporterModule } from 'mat-table-exporter';
import { MatMomentDateModule, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { JwPaginationComponent } from 'jw-angular-pagination';


@NgModule({
  declarations: [
    AppComponent,
    OrdersComponent,
    OrderComponent,
    OrderItemsComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    AdminPanelComponent,
    ForbiddenComponent,
    HomeComponent,
    EmployeeComponent,
    EmployeeListComponent,
    MatConfirmDialogComponent,
    JwPaginationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    Material.MatToolbarModule,
    Material.MatGridListModule,
    Material.MatFormFieldModule,
    Material.MatInputModule,
    Material.MatRadioModule,
    Material.MatSelectModule,
    Material.MatDatepickerModule,
    Material.MatNativeDateModule,
    Material.MatCheckboxModule,
    Material.MatButtonModule,
    Material.MatTableModule,
    Material.MatIconModule,
    Material.MatPaginatorModule,
    Material.MatSortModule,
    MatFormFieldModule,
    MatTableExporterModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatMomentDateModule,
    ToastrModule.forRoot()
  ],
  entryComponents:[OrderItemsComponent, EmployeeComponent, MatConfirmDialogComponent],
  providers: [OrderService,UserService,EmployeeService, 
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: {useUtc: true}}
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  
 }
