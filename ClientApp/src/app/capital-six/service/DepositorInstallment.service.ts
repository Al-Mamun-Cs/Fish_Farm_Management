import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DepositorInstallment } from '../../capital-six/models/DepositorInstallment';
import { IDepositorInstallmentPagination, DepositorInstallmentPagination } from '../../capital-six/models/DepositorInstallmentPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DepositorInstallmentService {

  baseUrl = environment.apiUrl;
  DepositorInstallments: DepositorInstallment[] = [];
  DepositorInstallmentPagination = new DepositorInstallmentPagination();
  constructor(private http: HttpClient) { }

  getDepositorInstallments(pageNumber, pageSize, searchText, warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IDepositorInstallmentPagination>(this.baseUrl + '/depositor-installment/get-DepositorInstallments', { observe: 'response', params })
      .pipe(
        map(response => {
          this.DepositorInstallments = [...this.DepositorInstallments, ...response.body.items];
          this.DepositorInstallmentPagination = response.body;
          return this.DepositorInstallmentPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<DepositorInstallment>(this.baseUrl + '/depositor-installment/get-DepositorInstallmentDetail/' + id);
  }

  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/depositor-installment/update-DepositorInstallment/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/depositor-installment/save-DepositorInstallment', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/depositor-installment/delete-DepositorInstallment/' + id);
  }

  getSelectedWarehousesList() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedDepositorList(warehouseId) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/depositor/get-selectedDepositors?warehouseId=' + warehouseId)
  }

  inAcctiveDepositorInstallment(id: number) {
    return this.http.get<DepositorInstallment>(this.baseUrl + '/depositor-installment/inActive-DepositorInstallment/' + id);
  }

  SpGetLastInstallmentMonthAndYear(depositorId) {
    return this.http.get<any[]>(this.baseUrl + '/depositor-installment/get-SpGetLastInstallmentMonthANDYear?depositorId=' + depositorId)
  }


}
