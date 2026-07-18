import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Brand } from '../models/Brand';
import { IBrandPagination, BrandPagination } from '../models/BrandPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  baseUrl = environment.apiUrl;
  Brands: Brand[] = [];
  BrandPagination = new BrandPagination();
  constructor(private http: HttpClient) { }

  getBrands(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IBrandPagination>(this.baseUrl + '/brand/get-Brands', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Brands = [...this.Brands, ...response.body.items];
        this.BrandPagination = response.body;
        return this.BrandPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Brand>(this.baseUrl + '/brand/get-BrandDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/brand/update-Brand/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/brand/save-Brand', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/brand/delete-Brand/'+id);
  }
  
}
