import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DepositorInvestment } from '../../capital-six/models/DepositorInvestment';
import { IDepositorInvestmentPagination, DepositorInvestmentPagination } from '../../capital-six/models/DepositorInvestmentPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DepositorInvestmentService {

  baseUrl = environment.apiUrl;
  DepositorInvestments: DepositorInvestment[] = [];
  DepositorInvestmentPagination = new DepositorInvestmentPagination();
  constructor(private http: HttpClient) { }

  getDepositorInvestments(pageNumber, pageSize, searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IDepositorInvestmentPagination>(this.baseUrl + '/depositor-investment/get-DepositorInvestments', { observe: 'response', params })
      .pipe(
        map(response => {
          this.DepositorInvestments = [...this.DepositorInvestments, ...response.body.items];
          this.DepositorInvestmentPagination = response.body;
          return this.DepositorInvestmentPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<DepositorInvestment>(this.baseUrl + '/depositor-investment/get-DepositorInvestmentDetail/' + id);
  }
  
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/depositor-investment/update-DepositorInvestment/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/depositor-investment/save-DepositorInvestment', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/depositor-investment/delete-DepositorInvestment/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedDepositorList(warehouseId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/depositor/get-selectedDepositors?warehouseId='+warehouseId)
  }
  
   inAcctiveDepositorInvestment(id: number) {
    return this.http.get<DepositorInvestment>(this.baseUrl + '/depositor-investment/inActive-DepositorInvestment/' + id);
  }

  


}
