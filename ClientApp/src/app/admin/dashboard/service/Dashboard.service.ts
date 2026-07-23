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

  getTotalDueAmountList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/supplier/get-SpGetTotalDueAmountList?warehouseId=' + warehouseId);
  }
  getTotalSupplierDueAmount(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/supplier/get-SpGetTotalSupplierDueAmount?warehouseId=' + warehouseId);
  }

  getTotalCustomerDueAmountList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/supplier/get-SpGetTotalCustomerDueAmountList?warehouseId=' + warehouseId);
  }
  getTotalCustomerDueAmount(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/supplier/get-SpGetTotalCustomerDueAmount?warehouseId=' + warehouseId);
  }

  getDailyTotalSalesAmount(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/shop-good-sale/get-SpGetDailyTotalSalesAmount?warehouseId=' + warehouseId);
  }
  getDailySaleAmountList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/shop-good-sale/get-SpGetDailySaleAmountList?warehouseId=' + warehouseId);
  }

  getDailyCostTotal(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/daily-miscellaneous-cost/get-SpGetDailyCostTotal?warehouseId=' + warehouseId);
  }
  getDailyCostDetailList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/daily-miscellaneous-cost/get-SpGetDailyCostDetailList?warehouseId=' + warehouseId);
  }
  getDailyAssetCostTotal(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/daily-miscellaneous-cost/get-SpGetDailyAssetCostTotal?warehouseId=' + warehouseId);
  }
  getDailyAssetCostDetailList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/daily-miscellaneous-cost/get-SpGetDailyAssetCostDetailList?warehouseId=' + warehouseId);
  }

  getTotalCashCapital(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/warehouse/get-SpGetTotalCashCapital?warehouseId=' + warehouseId);
  }
  getCashCapitalDetailList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/warehouse/get-SpGetCashCapitalDetail?warehouseId=' + warehouseId);
  }
  getTotalCashInHand(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/warehouse/get-SpGetTotalCashInHand?warehouseId=' + warehouseId);
  }
  getCashInHandDetail(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/warehouse/get-SpGetCashInHandDetail?warehouseId=' + warehouseId);
  }

  getTotalFisheriesProductTypeList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetTotalFisheriesProductTypeList?warehouseId=' + warehouseId);
  }

  getTotalFisheriesProductStockList(warehouseId, fisheriesProductTypeId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetFisheriesProductStockByIdList?warehouseId=' + warehouseId + '&fisheriesProductTypeId=' + fisheriesProductTypeId);
  }

  getTotalFisheriesPondList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/pond/get-SpGetTotalFisheriesPondList?warehouseId=' + warehouseId);
  }

  getTotalFisheriesProductTypewiseCoset(warehouseId, pondId) {
    return this.http.get<any[]>(this.baseUrl + '/pond/get-SpGetTotalFisheriesProductTypewiseCoset?warehouseId=' + warehouseId + '&pondId=' + pondId);
  }

  getTotalShopProductList(warehouseId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetTotalShopProduct?warehouseId=' + warehouseId);
  }

  getShopProductStockList(warehouseId, fisheriesProductTypeId) {
    return this.http.get<any[]>(this.baseUrl + '/fisheries-product-type/get-SpGetShopProductStockById?warehouseId=' + warehouseId + '&fisheriesProductTypeId=' + fisheriesProductTypeId);
  }


}
