import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Designation } from '../models/Designation';
import { IDesignationPagination, DesignationPagination } from '../models/DesignationPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DesignationService {

  baseUrl = environment.apiUrl;
  Designations: Designation[] = [];
  DesignationPagination = new DesignationPagination();
  constructor(private http: HttpClient) { }

  getDesignations(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IDesignationPagination>(this.baseUrl + '/designation/get-Designations', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Designations = [...this.Designations, ...response.body.items];
        this.DesignationPagination = response.body;
        return this.DesignationPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Designation>(this.baseUrl + '/designation/get-DesignationDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/designation/update-Designation/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/designation/save-Designation', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/designation/delete-Designation/'+id);
  }
  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  
}
