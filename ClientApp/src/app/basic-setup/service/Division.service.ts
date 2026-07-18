import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Division } from '../models/Division';
import {IDivisionPagination, DivisionPagination } from '../models/DivisionPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DivisionService {

  baseUrl = environment.apiUrl;
  Divisions: Division[] = [];
  DivisionPagination = new DivisionPagination();
  constructor(private http: HttpClient) { }

  getDivisions(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IDivisionPagination>(this.baseUrl + '/division/get-Divisions', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Divisions = [...this.Divisions, ...response.body.items];
        this.DivisionPagination = response.body;
        return this.DivisionPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Division>(this.baseUrl + '/division/get-DivisionDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/division/update-Division/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/division/save-Division', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/division/delete-Division/'+id);
  }
  
  getSelectedEasyBikeTypeList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/easy-bike-types/get-selectedEasyBikeTypes')
  }
}
