import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DailyMiscellaneousCost } from '../../financial-transactions/models/DailyMiscellaneousCost';
import { IDailyMiscellaneousCostPagination, DailyMiscellaneousCostPagination } from '../../financial-transactions/models/DailyMiscellaneousCostPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DailyMiscellaneousCostService {

  baseUrl = environment.apiUrl;
  DailyMiscellaneousCosts: DailyMiscellaneousCost[] = [];
  DailyMiscellaneousCostPagination = new DailyMiscellaneousCostPagination();
  constructor(private http: HttpClient) { }

  getDailyMiscellaneousCosts(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IDailyMiscellaneousCostPagination>(this.baseUrl + '/daily-miscellaneous-cost/get-DailyMiscellaneousCosts', { observe: 'response', params })
      .pipe(
        map(response => {
          this.DailyMiscellaneousCosts = [...this.DailyMiscellaneousCosts, ...response.body.items];
          this.DailyMiscellaneousCostPagination = response.body;
          return this.DailyMiscellaneousCostPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<DailyMiscellaneousCost>(this.baseUrl + '/daily-miscellaneous-cost/get-DailyMiscellaneousCostDetail/' + id);
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/daily-miscellaneous-cost/update-DailyMiscellaneousCost/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/daily-miscellaneous-cost/save-DailyMiscellaneousCost', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/daily-miscellaneous-cost/delete-DailyMiscellaneousCost/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
   getSelectedPaymentStausList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatuss')
  }

  getSelectedDailyCostReasonsList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/daily-cost-vaucher-reason/get-selectedDailyCostVaucherReasons?warehouseId='+warehouseId)
  }


}
