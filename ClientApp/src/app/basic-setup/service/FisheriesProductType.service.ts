import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FisheriesProductType } from '../models/FisheriesProductType';
import { IFisheriesProductTypePagination, FisheriesProductTypePagination } from '../models/FisheriesProductTypePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FisheriesProductTypeService {

  baseUrl = environment.apiUrl;
  FisheriesProductTypes: FisheriesProductType[] = [];
  FisheriesProductTypePagination = new FisheriesProductTypePagination();
  constructor(private http: HttpClient) { }

  getFisheriesProductTypes(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IFisheriesProductTypePagination>(this.baseUrl + '/fisheries-product-type/get-FisheriesProductTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.FisheriesProductTypes = [...this.FisheriesProductTypes, ...response.body.items];
        this.FisheriesProductTypePagination = response.body;
        return this.FisheriesProductTypePagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<FisheriesProductType>(this.baseUrl + '/fisheries-product-type/get-FisheriesProductTypeDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/fisheries-product-type/update-FisheriesProductType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fisheries-product-type/save-FisheriesProductType', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/fisheries-product-type/delete-FisheriesProductType/'+id);
  }
  
}
