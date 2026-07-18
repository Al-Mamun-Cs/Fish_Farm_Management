import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IUserPagination,UserPagination } from '../models/UserPagination';
import { User } from '../models/User';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.securityUrl;
  dropDownUrl = environment.apiUrl;
  Users: User[] = [];
  UserPagination = new UserPagination();
  constructor(private http: HttpClient) { }

  getUsers(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.http.get<IUserPagination>(this.baseUrl + '/users/get-users', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Users = [...this.Users, ...response.body.items];
        this.UserPagination = response.body;
        return this.UserPagination;
      })
    );
   
  }
  
  getUserByPno(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.http.get<IUserPagination>(this.baseUrl + '/Users/get-userByPno', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Users = [...this.Users, ...response.body.items];
        this.UserPagination = response.body;
        return this.UserPagination;
      })
    );
   
  }
  // getTraineeList(pno) {

  //   return this.http.get<any[]>(this.dropDownUrl + '/member-info/get-memberListForUserCreate?pno='+pno)
  //   .pipe(
  //     map(response => {
        
  //       return response;
  //     })
  //   ); 
   
  // }


  // getMembers(pageNumber, pageSize,searchText) {

  //   let params = new HttpParams();

  //   params = params.append('searchText', searchText.toString());
  //   params = params.append('pageNumber', pageNumber.toString());
  //   params = params.append('pageSize', pageSize.toString());

  //   return this.http.get<IUserPagination>(this.baseUrl + '/Users/get-membership-users', { observe: 'response', params })
  //   .pipe(
  //     map(response => {
  //       this.Users = [...this.Users, ...response.body.items];
  //       this.UserPagination = response.body;
  //       return this.UserPagination;
  //     })
  //   );
   
  // }
  

  // getTrainees(pageNumber, pageSize,searchText) {

  //   let params = new HttpParams();

  //   params = params.append('searchText', searchText.toString());
  //   params = params.append('pageNumber', pageNumber.toString());
  //   params = params.append('pageSize', pageSize.toString());

  //   return this.http.get<IUserPagination>(this.baseUrl + '/Users/get-student-users', { observe: 'response', params })
  //   .pipe(
  //     map(response => {
  //       this.Users = [...this.Users, ...response.body.items];
  //       this.UserPagination = response.body;
  //       return this.UserPagination;
  //     })
  //   );
   
  // }

 //autocomplete for By Pno  
 getSelectedPno(pno){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/sms/member-info/get-autocompletePno?pno='+pno)
    .pipe(
      map((response:[]) => response.map(item => item))
    )
}
  getselectedbranchinfo(){
    return this.http.get<SelectedModel[]>(this.dropDownUrl + '/branch-info/get-selectedBranchInfos')
  }


  getSelectedTraineeByPno(pno){
    return this.http.get<SelectedModel[]>(this.dropDownUrl + '/member-info/get-autocompletePno?pno='+pno)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  resetPassword(id:any, model: any) {
    return this.http.post(this.baseUrl + '/Users/reset-userPassword?userId='+id, model);
  } 

  find(id: string) {
    return this.http.get<User>(this.baseUrl + '/users/get-userDetail/' + id);
  }

  update(id:any, model: any) {
    return this.http.put(this.baseUrl + '/users/update-user?userId='+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/users/save-user', model);
  }  
  delete(id:number){
    return this.http.delete(this.baseUrl + '/users/delete-user/'+id);
  }
}
