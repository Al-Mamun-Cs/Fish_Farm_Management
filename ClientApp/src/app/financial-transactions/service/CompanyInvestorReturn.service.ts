import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyInvestorReturn } from '../../financial-transactions/models/CompanyInvestorReturn';
import { ICompanyInvestorReturnPagination, CompanyInvestorReturnPagination } from '../../financial-transactions/models/CompanyInvestorReturnPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class CompanyInvestorReturnService {

  baseUrl = environment.apiUrl;
  CompanyInvestorReturns: CompanyInvestorReturn[] = [];
  CompanyInvestorReturnPagination = new CompanyInvestorReturnPagination();
  constructor(private http: HttpClient) { }

  getCompanyInvestorReturns(pageNumber, pageSize, searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<ICompanyInvestorReturnPagination>(this.baseUrl + '/company-investor-return/get-CompanyInvestorReturns', { observe: 'response', params })
      .pipe(
        map(response => {
          this.CompanyInvestorReturns = [...this.CompanyInvestorReturns, ...response.body.items];
          this.CompanyInvestorReturnPagination = response.body;
          return this.CompanyInvestorReturnPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<CompanyInvestorReturn>(this.baseUrl + '/company-investor-return/get-CompanyInvestorReturnDetail/' + id);
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/company-investor-return/update-CompanyInvestorReturn/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/company-investor-return/save-CompanyInvestorReturn', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/company-investor-return/delete-CompanyInvestorReturn/' + id);
  }

  getSelectedWarehousesList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedInvestorList(warehouseId) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/company-investor/get-selectedCompanyInvestors?warehouseId=' + warehouseId)
  }
  getSelectedPaymentStausList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatuss')
  }
  inAcctiveCompanyInvestorReturn(id: number) {
    return this.http.get<any>(this.baseUrl + '/company-investor-return/inActive-CompanyInvestorReturn/' + id);
  }

}
