import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Religion } from '../models/Religion';
import {IReligionPagination, ReligionPagination } from '../models/ReligionPagination';

@Injectable({
  providedIn: 'root'
})
export class ReligionService {
 
  baseUrl = environment.apiUrl;
  Religions: Religion[] = [];
  ReligionPagination = new ReligionPagination();
  constructor(private http: HttpClient) { }

  getReligions(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IReligionPagination>(this.baseUrl + '/religion/get-Religions', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Religions = [...this.Religions, ...response.body.items];
        this.ReligionPagination = response.body;
        return this.ReligionPagination;
      })
    );
  }

  
  find(id: number) {
    return this.http.get<Religion>(this.baseUrl + '/religion/get-ReligionDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/religion/update-Religion/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/religion/save-Religion', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/religion/delete-Religion/'+id);
  }
  
  

}
