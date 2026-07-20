import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ShopGoodSale } from '../models/ShopGoodSale';
import {IShopGoodSalePagination, ShopGoodSalePagination } from '../models/ShopGoodSalePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ShopGoodSaleService {

  baseUrl = environment.apiUrl;
  ShopGoodSales: ShopGoodSale[] = [];
  ShopGoodSalePagination = new ShopGoodSalePagination();
  constructor(private http: HttpClient) { }

  getShopGoodSales(pageNumber, pageSize,searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IShopGoodSalePagination>(this.baseUrl + '/shop-good-sale/get-ShopGoodSales', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ShopGoodSales = [...this.ShopGoodSales, ...response.body.items];
        this.ShopGoodSalePagination = response.body;
        return this.ShopGoodSalePagination;
      })
    );
  }

  findShopDetail(id: number) {
    return this.http.get<any>(this.baseUrl + '/shop-inventory/get-ShopDetail/' + id);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/shop-good-sale/save-ShopGoodSale', model);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/inventory/update-inventory/'+id, model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/shop-good-sale/delete-ShopGoodSale/'+id);
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
    return this.http.get<any[]>(this.baseUrl + '/fisheries-unit/get-selectedFisheriesUnits')
  }
  GetShopInventoryProductName(warehouseId,fisheriesProductTypeId){ 
    return this.http.get<any>(this.baseUrl + '/shop-inventory/get-selectedShopInventoryProductName?warehouseId='+warehouseId+'&fisheriesProductTypeId='+fisheriesProductTypeId)
  }
  
  SpGetShopGoodSaleVoucherById(shopGoodSaleId){ 
    return this.http.get<any>(this.baseUrl + '/shop-good-sale/get-SpGetShopGoodSaleVoucherById?shopGoodSaleId='+shopGoodSaleId)
  }


  //autocomplete for SupplierName  
  getSelectedSupplierName(supplierName,warehouseId){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-AutoCompleteSupplierName?supplierName='+supplierName+'&warehouseId='+warehouseId)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  

  SpGetShopGoodSaleBillNo() {
    return this.http.get<any>( 
      this.baseUrl + "/shop-good-sale/get-SpGetShopGoodSaleBillNo");
    }
}
