import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Country } from '../models/Country';
import { ICountryPagination, CountryPagination } from '../models/CountryPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  baseUrl = environment.apiUrl;
  Countrys: Country[] = [];
  CountryPagination = new CountryPagination();
  constructor(private http: HttpClient) { }

  getCountrys(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<ICountryPagination>(this.baseUrl + '/country/get-Countrys', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Countrys = [...this.Countrys, ...response.body.items];
        this.CountryPagination = response.body;
        return this.CountryPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Country>(this.baseUrl + '/country/get-CountryDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/country/update-Country/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/country/save-Country', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/country/delete-Country/'+id);
  }
  
}
