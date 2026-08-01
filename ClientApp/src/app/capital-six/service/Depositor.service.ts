import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Depositor } from '../../capital-six/models/Depositor';
import { IDepositorPagination, DepositorPagination } from '../../capital-six/models/DepositorPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DepositorService {

  baseUrl = environment.apiUrl;
  Depositors: Depositor[] = [];
  DepositorPagination = new DepositorPagination();
  constructor(private http: HttpClient) { }

  getDepositors(pageNumber, pageSize, searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IDepositorPagination>(this.baseUrl + '/depositor/get-Depositors', { observe: 'response', params })
      .pipe(
        map(response => {
          this.Depositors = [...this.Depositors, ...response.body.items];
          this.DepositorPagination = response.body;
          return this.DepositorPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<Depositor>(this.baseUrl + '/depositor/get-DepositorDetail/' + id);
  }
  
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/depositor/update-Depositor/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/depositor/save-Depositor', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/depositor/delete-Depositor/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  


}
