import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FisheriesUnit } from '../models/FisheriesUnit';
import {IFisheriesUnitPagination, FisheriesUnitPagination } from '../models/FisheriesUnitPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class FisheriesUnitService {

  baseUrl = environment.apiUrl;
  FisheriesUnits: FisheriesUnit[] = [];
  FisheriesUnitPagination = new FisheriesUnitPagination();
  constructor(private http: HttpClient) { }

  getFisheriesUnits(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IFisheriesUnitPagination>(this.baseUrl + '/fisheries-unit/get-FisheriesUnits', { observe: 'response', params })
    .pipe(
      map(response => {
        this.FisheriesUnits = [...this.FisheriesUnits, ...response.body.items];
        this.FisheriesUnitPagination = response.body;
        return this.FisheriesUnitPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<FisheriesUnit>(this.baseUrl + '/fisheries-unit/get-FisheriesUnitDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/fisheries-unit/update-FisheriesUnit/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/fisheries-unit/save-FisheriesUnit', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/fisheries-unit/delete-FisheriesUnit/'+id);
  }
}
