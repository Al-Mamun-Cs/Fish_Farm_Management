import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { District } from '../models/District';
import {IDistrictPagination, DistrictPagination } from '../models/DistrictPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {

  baseUrl = environment.apiUrl;
  Districts: District[] = [];
  DistrictPagination = new DistrictPagination();
  constructor(private http: HttpClient) { }

  getDistricts(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IDistrictPagination>(this.baseUrl + '/district/get-Districts', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Districts = [...this.Districts, ...response.body.items];
        this.DistrictPagination = response.body;
        return this.DistrictPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<District>(this.baseUrl + '/district/get-DistrictDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/district/update-District/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/district/save-District', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/district/delete-District/'+id);
  }
  
  getSelectedDivisionList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/division/get-selectedDivisions')
  }
}
