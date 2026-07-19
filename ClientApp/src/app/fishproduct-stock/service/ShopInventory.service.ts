import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ShopInventory } from '../models/ShopInventory';
import {IShopInventoryPagination, ShopInventoryPagination } from '../models/ShopInventoryPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ShopInventoryService {

  baseUrl = environment.apiUrl;
  ShopInventorys: ShopInventory[] = [];
  ShopInventoryPagination = new ShopInventoryPagination();
  constructor(private http: HttpClient) { }

  getShopInventorys(pageNumber, pageSize,searchText, viewType,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());    
    params = params.append('viewType', viewType.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IShopInventoryPagination>(this.baseUrl + '/shop-inventory/get-ShopInventorys', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShopInventorys = [...this.ShopInventorys, ...response.body.items];
        this.ShopInventoryPagination = response.body;
        return this.ShopInventoryPagination;
      })
    );
  }

  
  submit(model: any) {
    return this.http.post(this.baseUrl + '/shop-inventory/save-ShopInventory', model);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/inventory/update-inventory/'+id, model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/shop-inventory/delete-ShopInventory/'+id);
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
  
  SpGetShopInventoryVoucherById(ShopInventoryId){ 
    return this.http.get<any>(this.baseUrl + '/shop-inventory/get-SpGetShopInventoryVoucherById?ShopInventoryId='+ShopInventoryId)
  }


  //autocomplete for SupplierName  
  getSelectedSupplierName(supplierName,warehouseId){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-AutoCompleteForBankTransaction?supplierName='+supplierName+'&warehouseId='+warehouseId)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  

  SpGetShopInventoryBillNo() {
    return this.http.get<any>( 
      this.baseUrl + "/shop-inventory/get-SpGetShopInventoryBillNo");
    }
}
