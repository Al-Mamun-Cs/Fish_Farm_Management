import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaymentStatus } from '../models/PaymentStatus';
import {IPaymentStatusPagination, PaymentStatusPagination } from '../models/PaymentStatusPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class PaymentStatusService {

  baseUrl = environment.apiUrl;
  PaymentStatuss: PaymentStatus[] = [];
  PaymentStatusPagination = new PaymentStatusPagination();
  constructor(private http: HttpClient) { }

  getPaymentStatuss(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IPaymentStatusPagination>(this.baseUrl + '/payment-status/get-paymentStatuss', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PaymentStatuss = [...this.PaymentStatuss, ...response.body.items];
        this.PaymentStatusPagination = response.body;
        return this.PaymentStatusPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<PaymentStatus>(this.baseUrl + '/payment-status/get-paymentStatusDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/payment-status/update-paymentStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/payment-status/save-paymentStatus', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/payment-status/delete-paymentStatus/'+id);
  }
  getSelectedPaymentStatusList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/payment-status/get-selectedPaymentStatuss')
  }
}
