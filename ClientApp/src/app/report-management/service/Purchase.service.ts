import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Salary } from '../models/Salary';
import {ISalaryPagination, SalaryPagination } from '../models/SalaryPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  
  getPurchaseListByWarehouse(dateFrom,dateTo, warehouseId, supplierId) {
    return this.http.get<any>( //get-spGetPurchaseListByWarehouse?dateFrom=2025-01-05&dateTo=2025-01-31&warehouseId=0&supplierId=0
      this.baseUrl + "/inventory/get-spGetPurchaseListByWarehouse?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&supplierId="+supplierId);
  }
  
  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  getSelectedEasyBikeBankList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/easy-bike-banks/get-selectedEasyBikeBanks')
  }
   getSelectedCategoryList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/easy-bike-types/get-selectedEasyBikeTypes')
  }

  getSaleListByWarehouse(dateFrom,dateTo, warehouseId,supplierId) {
    return this.http.get<any>(
      this.baseUrl + "/good-sale/get-GoodSaleListByWarehouse?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&supplierId="+supplierId);
  }
  SpGetAttendanceReport(dateFrom,dateTo, warehouseId,empolyeeId) {
    return this.http.get<any>(
      this.baseUrl + "/emp-attendance/get-SpGetAttendanceReport?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&empolyeeId="+empolyeeId);
  }

  SpGetLeaveReport(dateFrom,dateTo,warehouseId,empolyeeId) {
    return this.http.get<any>(
      this.baseUrl + "/employee-leave/get-SpGetLeaveReport?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&empolyeeId="+empolyeeId);
  }

  getBankTransactionListByWarehouse(dateFrom,dateTo, warehouseId, bankInfoId) {
    return this.http.get<any>(
      this.baseUrl + "/bank-transaction/get-SpBankTransactionListBywarehouse?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&bankInfoId="+bankInfoId);
  }
   getProduct( warehouseId, categoryId) {
    return this.http.get<any>(
      this.baseUrl + "/product-type/get-SpGetProductReport?warehouseId="+warehouseId+"&categoryId="+categoryId);
  }

  getDailyCostListByWarehouse(dateFrom,dateTo, warehouseId) {
    return this.http.get<any>(
      this.baseUrl + "/daily-cost/get-SpDailyCostListBywarehouse?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId);
  }
//due-daid/get-SpDuePaidListForReport?supplierId=2&warehouseId=1
  getSpDuePaidListForReport(supplierId, warehouseId) {
    return this.http.get<any>(
      this.baseUrl + "/due-daid/get-SpDuePaidListForReport?supplierId="+supplierId+"&warehouseId="+warehouseId);
  }

  getSpPayablePaidListForReport(dateFrom,dateTo,warehouseId,supplierId ) {
    return this.http.get<any>( //payable-paid/get-SpPayablePaidListForReport?dateFrom=2025-02-01&dateTo=2025-02-04&supplierId=0&warehouseId=0
      this.baseUrl + "/payable-paid/get-SpPayablePaidListForReport?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId+"&supplierId="+supplierId);
  }
  getSelectedSupplierByWarehouseIdList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-selectedSupplierByWarehouseId?warehouseId='+warehouseId)
  }

  getSpEmployeeListBywarehouse(dateFrom,dateTo,warehouseId ) {
    return this.http.get<any>( 
      this.baseUrl + "/salary/get-SpSalaryListForReport?dateFrom="+dateFrom+"&dateTo="+dateTo+"&warehouseId="+warehouseId);
  }
}
