import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Bank } from '../models/Bank';
import { IBankPagination, BankPagination } from '../models/BankPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class BankService {

  baseUrl = environment.apiUrl;
  Banks: Bank[] = [];
  BankPagination = new BankPagination();
  constructor(private http: HttpClient) { }

  getBanks(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IBankPagination>(this.baseUrl + '/bank/get-Banks', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Banks = [...this.Banks, ...response.body.items];
        this.BankPagination = response.body;
        return this.BankPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Bank>(this.baseUrl + '/bank/get-BankDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/bank/update-Bank/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/bank/save-Bank', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/bank/delete-Bank/'+id);
  }
  
}
