import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FisheriesProductReturn } from '../../fishproduct-stock/models/FisheriesProductReturn';
import { IFisheriesProductReturnPagination, FisheriesProductReturnPagination } from '../../fishproduct-stock/models/FisheriesProductReturnPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FisheriesProductReturnService {

  baseUrl = environment.apiUrl;
  FisheriesProductReturns: FisheriesProductReturn[] = [];
  FisheriesProductReturnPagination = new FisheriesProductReturnPagination();
  constructor(private http: HttpClient) { }

  getFisheriesProductReturns(pageNumber, pageSize, searchText, warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IFisheriesProductReturnPagination>(this.baseUrl + '/fisheries-product-return/get-FisheriesProductReturns', { observe: 'response', params })
      .pipe(
        map(response => {
          this.FisheriesProductReturns = [...this.FisheriesProductReturns, ...response.body.items];
          this.FisheriesProductReturnPagination = response.body;
          return this.FisheriesProductReturnPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<FisheriesProductReturn>(this.baseUrl + '/fisheries-product-return/get-FisheriesProductReturnDetail/' + id);
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/fisheries-product-return/update-FisheriesProductReturn/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fisheries-product-return/save-FisheriesProductReturn', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/fisheries-product-return/delete-FisheriesProductReturn/' + id);
  }

  inAcctiveFisheriesProductReturn(id: number) {
    return this.http.get<FisheriesProductReturn>(this.baseUrl + '/fisheries-product-return/inActive-FisheriesProductReturn/' + id);
  }

  getSelectedWarehousesList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedProductTypeList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-product-type/get-selectedProductTypeForFisheries?warehouseId='+warehouseId)
  }

  getSelectedProduct(warehouseId, fisheriesProductTypeId) { 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-inventory/get-AutoCompleteProductName?warehouseId=' + warehouseId + '&fisheriesProductTypeId=' + fisheriesProductTypeId)
      .pipe(
        map((response: []) => response.map(item => item))
      )
  }

  getSelectedSupplierList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-selectedSupplierByWarehouseIdForKroy?warehouseId='+warehouseId)
  }
  
  



}
