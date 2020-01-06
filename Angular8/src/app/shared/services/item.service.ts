import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Item } from '../../shared/models/item.model';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http:HttpClient) { }

  getItemList():Observable<Item[]>{
    return this.http.get<Item[]>(environment.apiURL+'/Item');
}

}
