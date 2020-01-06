import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Department } from '../../shared/models/department.model';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private http:HttpClient) { }

  getDepartmentList():Observable<Department[]>{
    return this.http.get<Department[]>(environment.apiURL+'/Department');
  }

}
