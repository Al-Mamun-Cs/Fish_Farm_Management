import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FishSale } from '../../stock-consumption/models/FishSale';
import { IFishSalePagination, FishSalePagination } from '../../stock-consumption/models/FishSalePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FishSaleService {

  baseUrl = environment.apiUrl;
  FishSales: FishSale[] = [];
  FishSalePagination = new FishSalePagination();
  constructor(private http: HttpClient) { }

  getFishSales(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IFishSalePagination>(this.baseUrl + '/fish-sale/get-FishSales', { observe: 'response', params })
      .pipe(
        map(response => {
          this.FishSales = [...this.FishSales, ...response.body.items];
          this.FishSalePagination = response.body;
          return this.FishSalePagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<FishSale>(this.baseUrl + '/fish-sale/get-FishSaleDetail/' + id);
  }
  
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/fish-sale/update-FishSale/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fish-sale/save-FishSale', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/fish-sale/delete-FishSale/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  getSelectedPondList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/pond/get-selectedPonds')
  }

  getSelectedSupplierList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-selectedSupplierByWarehouseIdForBikroy?warehouseId='+warehouseId)
  }

  getSelectedUnitList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fisheries-unit/get-selectedFisheriesUnits')
  }
  getSelectedPaymentStausList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatuss')
  }


}
