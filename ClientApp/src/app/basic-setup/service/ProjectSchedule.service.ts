import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProjectSchedule } from '../models/ProjectSchedule';
import { IProjectSchedulePagination, ProjectSchedulePagination } from '../models/ProjectSchedulePagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ProjectScheduleService {

  baseUrl = environment.apiUrl;
  ProjectSchedules: ProjectSchedule[] = [];
  ProjectSchedulePagination = new ProjectSchedulePagination();
  constructor(private http: HttpClient) { }

  getProjectSchedules(pageNumber, pageSize,searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IProjectSchedulePagination>(this.baseUrl + '/project-schedule/get-ProjectSchedules', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProjectSchedules = [...this.ProjectSchedules, ...response.body.items];
        this.ProjectSchedulePagination = response.body;
        return this.ProjectSchedulePagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<ProjectSchedule>(this.baseUrl + '/project-schedule/get-ProjectScheduleDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/project-schedule/update-ProjectSchedule/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/project-schedule/save-ProjectSchedule', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/project-schedule/delete-ProjectSchedule/'+id);
  }

   getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }

  getSelectedPondList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/pond/get-selectedPonds')
  }
  
}
