import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Fiscalyear } from '../models/Fiscalyear';
import { IFiscalyearPagination, FiscalyearPagination } from '../models/FiscalyearPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FiscalyearService {

  baseUrl = environment.apiUrl;
  Fiscalyears: Fiscalyear[] = [];
  FiscalyearPagination = new FiscalyearPagination();
  constructor(private http: HttpClient) { }

  getFiscalyears(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IFiscalyearPagination>(this.baseUrl + '/fiscal-year/get-Fiscalyears', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Fiscalyears = [...this.Fiscalyears, ...response.body.items];
        this.FiscalyearPagination = response.body;
        return this.FiscalyearPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Fiscalyear>(this.baseUrl + '/fiscal-year/get-FiscalyearDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/fiscal-year/update-Fiscalyear/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fiscal-year/save-Fiscalyear', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/fiscal-year/delete-Fiscalyear/'+id);
  }
  
}
