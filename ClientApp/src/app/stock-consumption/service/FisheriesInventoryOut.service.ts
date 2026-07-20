import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FisheriesInventoryOut } from '../../stock-consumption/models/FisheriesInventoryOut';
import { IFisheriesInventoryOutPagination, FisheriesInventoryOutPagination } from '../../stock-consumption/models/FisheriesInventoryOutPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FisheriesInventoryOutService {

  baseUrl = environment.apiUrl;
  FisheriesInventoryOuts: FisheriesInventoryOut[] = [];
  FisheriesInventoryOutPagination = new FisheriesInventoryOutPagination();
  constructor(private http: HttpClient) { }

  getFisheriesInventoryOuts(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IFisheriesInventoryOutPagination>(this.baseUrl + '/fisheries-inventory-out/get-FisheriesInventoryOuts', { observe: 'response', params })
      .pipe(
        map(response => {
          this.FisheriesInventoryOuts = [...this.FisheriesInventoryOuts, ...response.body.items];
          this.FisheriesInventoryOutPagination = response.body;
          return this.FisheriesInventoryOutPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<FisheriesInventoryOut>(this.baseUrl + '/fisheries-inventory-out/get-FisheriesInventoryOutDetail/' + id);
  }
  
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/fisheries-inventory-out/update-FisheriesInventoryOut/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fisheries-inventory-out/save-FisheriesInventoryOut', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/fisheries-inventory-out/delete-FisheriesInventoryOut/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  getSelectedPondList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/pond/get-selectedPonds')
  }

  getSelectedProductTypeList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-product-type/get-selectedFisheriesProductTypes?warehouseId='+warehouseId)
  }

  //autocomplete for Product  
  getSelectedProduct(productName,warehouseId,fisheriesProductTypeId) { //fisheries-inventory/get-AutoCompleteProductName?productName=g&warehouseId=36&fisheriesProductTypeId=1
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-inventory/get-AutoCompleteProductName?productName='+productName+'&warehouseId='+warehouseId+'&fisheriesProductTypeId='+fisheriesProductTypeId)
      .pipe(
        map((response: []) => response.map(item => item))
      )
  }

}
