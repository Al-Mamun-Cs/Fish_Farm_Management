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

  
  SPSupplierLedgerReport(supplierId,dateFrom,dateTo ) {
    return this.http.get<any>( 
      this.baseUrl + "/supplier/get-SP_SupplierLedgerReport?supplierId="+supplierId+"&dateFrom="+dateFrom+"&dateTo="+dateTo);
  }
  GetSupplierList(warehouseId,supplierStatus) {
    return this.http.get<any>( 
      this.baseUrl + "/supplier/get-selectedSuppliers?warehouseId="+warehouseId+"&supplierStatus="+supplierStatus);
  }

  
}
