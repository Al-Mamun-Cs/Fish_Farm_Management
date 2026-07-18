import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IOnlineChatPagination,OnlineChatPagination } from '../models/OnlineChatPagination';
import { OnlineChat } from '../models/OnlineChat';
import { SelectedModel } from '../../core/models/selectedModel';
import { map } from 'rxjs';
import { PostResponse } from 'src/app/core/models/PostResponse';

@Injectable({
  providedIn: 'root'
})
export class OnlineChatService {
  baseUrl = environment.apiUrl;
  OnlineChats: OnlineChat[] = [];
  OnlineChatPagination = new OnlineChatPagination(); 
  constructor(private http: HttpClient) { }

  getCourseByBaseSchoolNameId(baseSchoolNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/course-name/get-selectedCourseByBaseNameId?baseSchoolNameId='+baseSchoolNameId)
  }
  

  getOnlineChatsBySchool(userRole,senderId,reciverId){
    return this.http.get<OnlineChat[]>(this.baseUrl + '/online-chat/get-OnlineChatsById?userRole='+userRole+'&senderId='+senderId+'&reciverId='+reciverId)
  }
  getOnlineChatReminderForAdmin(baseSchoolNameId,userRole){
    return this.http.get<any[]>(this.baseUrl + '/online-chat/get-OnlineChatReminderForAdmin?id='+baseSchoolNameId+'&userRole='+userRole)
  }

  getOnlineChatResponselistForSchool(baseSchoolNameId){
    return this.http.get<any[]>(this.baseUrl + '/online-chat/get-OnlineChatResponselistForSchool?id='+baseSchoolNameId)
  }

  ChangeOnlineChatSeenStatus(OnlineChatId, status) {
    return this.http.get<any>(this.baseUrl + '/online-chat/change-recieverSeenStatus?OnlineChatId='+OnlineChatId+'&Status='+status);
  }

  getOnlineChats(pageNumber, pageSize,searchText) {

    let params = new HttpParams(); 
    
    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
   
    return this.http.get<IOnlineChatPagination>(this.baseUrl + '/online-chat/get-OnlineChats', { observe: 'response', params })
    .pipe(
      map(response => {
        this.OnlineChats = [...this.OnlineChats, ...response.body.items];
        this.OnlineChatPagination = response.body;
        return this.OnlineChatPagination;
      })
    ); 
  }

  find(id: number) {
    return this.http.get<OnlineChat>(this.baseUrl + '/online-chat/get-OnlineChatDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/online-chat/update-OnlineChat/'+id, model);
  }
  submit(model: any) {
    return this.http.post<PostResponse>(this.baseUrl + '/online-chat/save-OnlineChat', model).pipe(
      map((BnaClassTest: PostResponse) => {
        if (BnaClassTest) {
          console.log(BnaClassTest);
          return BnaClassTest;
        }
      })
    );
  
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/online-chat/delete-OnlineChat/'+id);
  }
}
