import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from '../../shared/models/customer.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  getCustomerList():Observable<Customer[]>{
    return this.http.get<Customer[]>(environment.apiURL+'/Customer');
  }

}
