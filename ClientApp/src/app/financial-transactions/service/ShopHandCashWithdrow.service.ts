import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ShopHandCashWithdrow } from '../../financial-transactions/models/ShopHandCashWithdrow';
import { IShopHandCashWithdrowPagination, ShopHandCashWithdrowPagination } from '../../financial-transactions/models/ShopHandCashWithdrowPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ShopHandCashWithdrowService {

  baseUrl = environment.apiUrl;
  ShopHandCashWithdrows: ShopHandCashWithdrow[] = [];
  ShopHandCashWithdrowPagination = new ShopHandCashWithdrowPagination();
  constructor(private http: HttpClient) { }

  getShopHandCashWithdrows(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IShopHandCashWithdrowPagination>(this.baseUrl + '/shop-hand-cash-withdrow/get-ShopHandCashWithdrows', { observe: 'response', params })
      .pipe(
        map(response => {
          this.ShopHandCashWithdrows = [...this.ShopHandCashWithdrows, ...response.body.items];
          this.ShopHandCashWithdrowPagination = response.body;
          return this.ShopHandCashWithdrowPagination;
        })
      );
  }
  find(id: number) {
    return this.http.get<ShopHandCashWithdrow>(this.baseUrl + '/shop-hand-cash-withdrow/get-ShopHandCashWithdrowDetail/' + id);
  }
  update(id: number, model: any) {
    return this.http.put(this.baseUrl + '/shop-hand-cash-withdrow/update-ShopHandCashWithdrow/' + id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/shop-hand-cash-withdrow/save-ShopHandCashWithdrow', model);
  }
  delete(id) {
    return this.http.delete(this.baseUrl + '/shop-hand-cash-withdrow/delete-ShopHandCashWithdrow/' + id);
  }

  getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
   

  inAcctiveWithdrow(id: number) {
      return this.http.get<any>(this.baseUrl + '/shop-hand-cash-withdrow/inActive-ShopHandCashWithdrow/' + id);
    }


}
