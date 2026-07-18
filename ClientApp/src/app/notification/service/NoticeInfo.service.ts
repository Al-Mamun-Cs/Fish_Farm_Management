import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NoticeInfo } from '../models/NoticeInfo';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import {INoticeInfoPagination, NoticeInfoPagination } from '../models/NoticeInfoPagination'

@Injectable({
  providedIn: 'root'
})
export class NoticeInfoService {

  baseUrl = environment.apiUrl;
  NoticeInfos: NoticeInfo[] = [];
  NoticeInfoPagination = new NoticeInfoPagination();
  constructor(private http: HttpClient) { }

  getNoticeInfos(pageNumber, pageSize,searchText,noticeTypeId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('noticeTypeId', noticeTypeId.toString());
    return this.http.get<INoticeInfoPagination>(this.baseUrl + '/notice-info/get-NoticeInfos', { observe: 'response', params })
    .pipe(
      map(response => {
        this.NoticeInfos = [...this.NoticeInfos, ...response.body.items];
        this.NoticeInfoPagination = response.body;
        return this.NoticeInfoPagination;
      })
    );
  }

  getNoticeInfoByMember(pageNumber, pageSize,searchText,memberInfoId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('memberInfoId', memberInfoId.toString());
    return this.http.get<INoticeInfoPagination>(this.baseUrl + '/notice-info/get-NoticeInfoByMember', { observe: 'response', params })
    .pipe(
      map(response => {
        this.NoticeInfos = [...this.NoticeInfos, ...response.body.items];
        this.NoticeInfoPagination = response.body;
        return this.NoticeInfoPagination;
      })
    );
  }
  getSelectedPno(pno){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/member-info/get-autocompletePno?pno='+pno)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getselectedNoticeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/notice-type/get-selectedNoticeTypes')
  }
  find(id: number) {
    return this.http.get<NoticeInfo>(this.baseUrl + '/notice-info/get-NoticeInfoDetail/' + id);
  }
  updateNoticeInfoListByMember(memberInfoId) {
    return this.http.get<any[]>(this.baseUrl + '/notice-info/update-noticeInfoListByMember?memberInfoId=' + memberInfoId);
  }
  updateNoticeInfoDetailByMember(noticeInfoId) {
    return this.http.get<any[]>(this.baseUrl + '/notice-info/update-noticeInfoDetailByMember?noticeInfoId=' + noticeInfoId);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/notice-info/update-NoticeInfo/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/notice-info/save-NoticeInfo', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/notice-info/delete-NoticeInfo/'+id);
  }
}
