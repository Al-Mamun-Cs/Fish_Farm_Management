import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pond } from '../models/Pond';
import { IPondPagination, PondPagination } from '../models/PondPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class PondService {

  baseUrl = environment.apiUrl;
  Ponds: Pond[] = [];
  PondPagination = new PondPagination();
  constructor(private http: HttpClient) { }

  getPonds(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IPondPagination>(this.baseUrl + '/pond/get-Ponds', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Ponds = [...this.Ponds, ...response.body.items];
        this.PondPagination = response.body;
        return this.PondPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Pond>(this.baseUrl + '/pond/get-PondDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/pond/update-Pond/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/pond/save-Pond', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/pond/delete-Pond/'+id);
  }
  
}
