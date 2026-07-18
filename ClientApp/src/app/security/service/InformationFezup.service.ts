import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InformationFezup } from '../models/InformationFezup';
import {IInformationFezupPagination, InformationFezupPagination } from '../models/InformationFezupPagination'

@Injectable({
  providedIn: 'root'
})
export class InformationFezupService {

  baseUrl = environment.apiUrl;
  InformationFezups: InformationFezup[] = [];
  InformationFezupPagination = new InformationFezupPagination();
  constructor(private http: HttpClient) { }

  getInformationFezups(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IInformationFezupPagination>(this.baseUrl + '/information-fezup/get-InformationFezups', { observe: 'response', params })
    .pipe(
      map(response => {
        this.InformationFezups = [...this.InformationFezups, ...response.body.items];
        this.InformationFezupPagination = response.body;
        return this.InformationFezupPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<InformationFezup>(this.baseUrl + '/information-fezup/get-InformationFezupDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/information-fezup/update-InformationFezup/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/information-fezup/save-InformationFezup', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/information-fezup/delete-InformationFezup/'+id);
  }
}
