import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { SelectedModel } from "src/app/core/models/selectedModel";
import { map } from "rxjs";
@Injectable({
  providedIn: "root",
})
export class DashboardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }



  getTotalFisheriesProductTypeList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetTotalFisheriesProductTypeList?warehouseId='+warehouseId);
  }
 
  getTotalFisheriesProductStockList(warehouseId,fisheriesProductTypeId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetFisheriesProductStockByIdList?warehouseId='+warehouseId+'&fisheriesProductTypeId='+fisheriesProductTypeId);
  }

  getTotalFisheriesPondList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/pond/get-SpGetTotalFisheriesPondList?warehouseId='+warehouseId);
  }

  getTotalFisheriesProductTypewiseCoset(warehouseId,pondId) {
    return this.http.get<any[]>(this.baseUrl + '/pond/get-SpGetTotalFisheriesProductTypewiseCoset?warehouseId='+warehouseId+'&pondId='+pondId);
  }
  

}
