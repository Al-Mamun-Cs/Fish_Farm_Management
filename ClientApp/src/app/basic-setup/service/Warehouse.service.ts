import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Warehouse } from '../models/Warehouse';
import {IWarehousePagination, WarehousePagination } from '../models/WarehousePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  baseUrl = environment.apiUrl;
  Warehouses: Warehouse[] = [];
  WarehousePagination = new WarehousePagination();
  constructor(private http: HttpClient) { }

  getWarehouses(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IWarehousePagination>(this.baseUrl + '/warehouse/get-Warehouses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Warehouses = [...this.Warehouses, ...response.body.items];
        this.WarehousePagination = response.body;
        return this.WarehousePagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Warehouse>(this.baseUrl + '/warehouse/get-WarehouseDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/warehouse/update-Warehouse/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/warehouse/save-Warehouse', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/warehouse/delete-Warehouse/'+id);
  }
  
  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  getSelectedPostPositionList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-post-position/get-selectedDepartmentPostPositions?warehouseId='+warehouseId)
  }

  // getSelectedCategoryList(itemProductionTypeId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/easy-bike-types/get-selectedEasyBikeTypes?itemProductionTypeId='+itemProductionTypeId)
  // }
}
