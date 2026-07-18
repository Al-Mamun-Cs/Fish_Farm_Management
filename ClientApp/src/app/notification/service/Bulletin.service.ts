import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { environment } from 'src/environments/environment';
import { Bulletin } from '../models/Bulletin';
import {IBulletinPagination, BulletinPagination } from '../models/BulletinPagination'

@Injectable({
  providedIn: 'root'
})
export class BulletinService {

  baseUrl = environment.apiUrl;
  Bulletins: Bulletin[] = [];
  BulletinPagination = new BulletinPagination();
  constructor(private http: HttpClient) { }

  getBulletins(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IBulletinPagination>(this.baseUrl + '/bulletin/get-Bulletins', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Bulletins = [...this.Bulletins, ...response.body.items];
        this.BulletinPagination = response.body;
        return this.BulletinPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<Bulletin>(this.baseUrl + '/bulletin/get-BulletinDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/bulletin/update-Bulletin/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/bulletin/save-Bulletin', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/bulletin/delete-Bulletin/'+id);
  }

  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
}
