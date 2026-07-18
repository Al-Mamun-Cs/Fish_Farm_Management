import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Supplier } from '../models/Supplier';
import {ISupplierPagination, SupplierPagination } from '../models/SupplierPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  baseUrl = environment.apiUrl;
  Suppliers: Supplier[] = [];
  SupplierPagination = new SupplierPagination();
  constructor(private http: HttpClient) { }

  getSuppliers(pageNumber, pageSize,searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<ISupplierPagination>(this.baseUrl + '/supplier/get-suppliers', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Suppliers = [...this.Suppliers, ...response.body.items];
        this.SupplierPagination = response.body;
        return this.SupplierPagination;
      })
    );
  }

  getSupplierByStatus(pageNumber, pageSize,searchText,warehouseId,supplierStatus) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    params = params.append('supplierStatus', supplierStatus.toString());
    return this.http.get<ISupplierPagination>(this.baseUrl + '/supplier/get-suppliersByStatus', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Suppliers = [...this.Suppliers, ...response.body.items];
        this.SupplierPagination = response.body;
        return this.SupplierPagination;
      })
    );
  }
  
  getSuppliersForBikroy(pageNumber, pageSize,searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<ISupplierPagination>(this.baseUrl + '/supplier/get-suppliersForBikroy', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Suppliers = [...this.Suppliers, ...response.body.items];
        this.SupplierPagination = response.body;
        return this.SupplierPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Supplier>(this.baseUrl + '/supplier/get-supplierDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/supplier/update-supplier/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/supplier/save-supplier', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/supplier/delete-supplier/'+id);
  }
  
  getSelectedSupplierList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier/get-selectedSuppliers')
  }
  getSelectedProductTypeList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/product-type/get-selectedSuppliers')
  }
  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
//supplier/get-phoneNoIsExistCheck?phoneNo
  getPhoneNOIsExistCheck(phoneNo){
    return this.http.get<boolean>(this.baseUrl + '/supplier/get-phoneNoIsExistCheck?phoneNo='+phoneNo+'')
  }
  // getSelectedCustomerTypeList(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/customer-type/get-selectedCustomerTypes')
  // }

  getSelectedSupplierTypeList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/supplier-type/get-selectedSupplierTypes')
  }

  getSelectedDivisionList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-selectedDivisions')
  }
 getSelectedDistrictList(divisionId){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-selectedDistricts?divisionId='+divisionId)
  }
   getSelectedUpozilaList(districtId){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/upozila/get-selectedUpozilas?districtId='+districtId)
  }

   getSelectedCountryList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/country/get-selectedCountrys')
  }

  //autocomplete for districtName   
  getSelectedDistrictName(districtName){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/district/get-AutoCompleteDistrictName?districtName='+districtName)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  //autocomplete for UpazilaName  
  getSelectedUpazilaName(upazilaName){ 
    return this.http.get<SelectedModel[]>(this.baseUrl + '/upozila/get-AutoCompleteUpazilaName?upazilaName='+upazilaName)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }
}
