import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InvestmentIncome } from '../../capital-six/models/InvestmentIncome';
import { IInvestmentIncomePagination, InvestmentIncomePagination } from '../../capital-six/models/InvestmentIncomePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class InvestmentIncomeService {

  baseUrl = environment.apiUrl;
  InvestmentIncomes: InvestmentIncome[] = [];
  InvestmentIncomePagination = new InvestmentIncomePagination();
  constructor(private http: HttpClient) { }

  getInvestmentIncomes(pageNumber, pageSize, searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IInvestmentIncomePagination>(this.baseUrl + '/investment-income/get-InvestmentIncomes', { observe: 'response', params })
      .pipe(
        map(response => {
          this.InvestmentIncomes = [...this.InvestmentIncomes, ...response.body.items];
          this.InvestmentIncomePagination = response.body;
          return this.InvestmentIncomePagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<InvestmentIncome>(this.baseUrl + '/investment-income/get-InvestmentIncomeDetail/' + id);
  }
  
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/investment-income/update-InvestmentIncome/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/investment-income/save-InvestmentIncome', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/investment-income/delete-InvestmentIncome/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedDepositorInvestmentList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/depositor-investment/get-selectedDepositorInvestments?warehouseId='+warehouseId)
  }
  
   inAcctiveInvestmentIncome(id: number) {
    return this.http.get<InvestmentIncome>(this.baseUrl + '/investment-income/inActive-InvestmentIncome/' + id);
  }

  


}
