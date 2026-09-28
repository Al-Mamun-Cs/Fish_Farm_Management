import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProjectType } from '../models/ProjectType';
import { IProjectTypePagination, ProjectTypePagination } from '../models/ProjectTypePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ProjectTypeService {

  baseUrl = environment.apiUrl;
  ProjectTypes: ProjectType[] = [];
  ProjectTypePagination = new ProjectTypePagination();
  constructor(private http: HttpClient) { }

  getProjectTypes(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IProjectTypePagination>(this.baseUrl + '/project-type/get-ProjectTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProjectTypes = [...this.ProjectTypes, ...response.body.items];
        this.ProjectTypePagination = response.body;
        return this.ProjectTypePagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<ProjectType>(this.baseUrl + '/project-type/get-ProjectTypeDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/project-type/update-ProjectType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/project-type/save-ProjectType', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/project-type/delete-ProjectType/'+id);
  }
  
}
