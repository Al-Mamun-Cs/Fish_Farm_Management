import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyInvestor } from '../../financial-transactions/models/CompanyInvestor';
import { ICompanyInvestorPagination, CompanyInvestorPagination } from '../../financial-transactions/models/CompanyInvestorPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class CompanyInvestorService {

  baseUrl = environment.apiUrl;
  CompanyInvestors: CompanyInvestor[] = [];
  CompanyInvestorPagination = new CompanyInvestorPagination();
  constructor(private http: HttpClient) { }

  getCompanyInvestors(pageNumber, pageSize, searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<ICompanyInvestorPagination>(this.baseUrl + '/company-investor/get-CompanyInvestors', { observe: 'response', params })
      .pipe(
        map(response => {
          this.CompanyInvestors = [...this.CompanyInvestors, ...response.body.items];
          this.CompanyInvestorPagination = response.body;
          return this.CompanyInvestorPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<CompanyInvestor>(this.baseUrl + '/company-investor/get-CompanyInvestorDetail/' + id);
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/company-investor/update-CompanyInvestor/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/company-investor/save-CompanyInvestor', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/company-investor/delete-CompanyInvestor/' + id);
  }

  getSelectedWarehousesList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  

}
