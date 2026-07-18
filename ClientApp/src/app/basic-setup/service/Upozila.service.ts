import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Upozila } from '../models/Upozila';
import {IUpozilaPagination, UpozilaPagination } from '../models/UpozilaPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class UpozilaService {

  baseUrl = environment.apiUrl;
  Upozilas: Upozila[] = [];
  UpozilaPagination = new UpozilaPagination();
  constructor(private http: HttpClient) { }

  getUpozilas(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IUpozilaPagination>(this.baseUrl + '/upozila/get-Upozilas', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Upozilas = [...this.Upozilas, ...response.body.items];
        this.UpozilaPagination = response.body;
        return this.UpozilaPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Upozila>(this.baseUrl + '/upozila/get-UpozilaDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/upozila/update-Upozila/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/upozila/save-Upozila', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/upozila/delete-Upozila/'+id);
  }
  
  getSelectedDistrictList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-selectedDistrictForUpazila')
  }
}
