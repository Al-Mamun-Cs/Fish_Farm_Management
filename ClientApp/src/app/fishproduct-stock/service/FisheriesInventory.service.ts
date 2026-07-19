import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FisheriesInventory } from '../models/FisheriesInventory';
import {IFisheriesInventoryPagination, FisheriesInventoryPagination } from '../models/FisheriesInventoryPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FisheriesInventoryService {

  baseUrl = environment.apiUrl;
  FisheriesInventorys: FisheriesInventory[] = [];
  FisheriesInventoryPagination = new FisheriesInventoryPagination();
  constructor(private http: HttpClient) { }

  getFisheriesInventorys(pageNumber, pageSize,searchText, viewType,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());    
    params = params.append('viewType', viewType.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IFisheriesInventoryPagination>(this.baseUrl + '/fisheries-inventory/get-FisheriesInventorys', { observe: 'response', params })
    .pipe(
      map(response => {
        this.FisheriesInventorys = [...this.FisheriesInventorys, ...response.body.items];
        this.FisheriesInventoryPagination = response.body;
        return this.FisheriesInventoryPagination;
      })
    );
  }

  
  // find(id: number) {
  //   return this.http.get<FisheriesInventory>(this.baseUrl + '/fisheries-inventory/get-FisheriesInventoryDetail/' + id);
  // }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fisheries-inventory/save-FisheriesInventory', model);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/inventory/update-inventory/'+id, model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/fisheries-inventory/delete-FisheriesInventory/'+id);
  }
  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  
  getSelectedPaymentStausList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatuss')
  }

  getSelectedProductTypeList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-product-type/get-selectedFisheriesProductTypes?warehouseId='+warehouseId)
  }

  getSelectedUnitList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-unit/get-selectedFisheriesUnits')
  }
  
  getSpInventoryVoucherByInventoryId(fisheriesInventoryId){ 
    return this.http.get<any>(this.baseUrl + '/fisheries-inventory/get-SpGetFisheriesInventoryVoucherById?fisheriesInventoryId='+fisheriesInventoryId)
  }


  //autocomplete for SupplierName  
  getSelectedSupplierName(supplierName,warehouseId){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-AutoCompleteForBankTransaction?supplierName='+supplierName+'&warehouseId='+warehouseId)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  

  getPurchaBillNo() {
    return this.http.get<any>( 
      this.baseUrl + "/fisheries-inventory/get-SpGetFisheriesBillNo");
    }
}
