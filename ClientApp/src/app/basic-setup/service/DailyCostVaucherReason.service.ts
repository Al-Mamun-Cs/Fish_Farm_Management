import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DailyCostVaucherReason } from '../models/DailyCostVaucherReason';
import { IDailyCostVaucherReasonPagination, DailyCostVaucherReasonPagination } from '../models/DailyCostVaucherReasonPagination';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class DailyCostVaucherReasonService {

  baseUrl = environment.apiUrl;
  DailyCostVaucherReasons: DailyCostVaucherReason[] = [];
  DailyCostVaucherReasonPagination = new DailyCostVaucherReasonPagination();
  constructor(private http: HttpClient) { }

  getDailyCostVaucherReasons(pageNumber, pageSize,searchText,warehouseId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('warehouseId', warehouseId.toString());
    return this.http.get<IDailyCostVaucherReasonPagination>(this.baseUrl + '/daily-cost-vaucher-reason/get-DailyCostVaucherReasons', { observe: 'response', params })
    .pipe(
      map(response => {
        this.DailyCostVaucherReasons = [...this.DailyCostVaucherReasons, ...response.body.items];
        this.DailyCostVaucherReasonPagination = response.body;
        return this.DailyCostVaucherReasonPagination;
      })
    );
  }
  find(id: number) {
    return this.http.get<DailyCostVaucherReason>(this.baseUrl + '/daily-cost-vaucher-reason/get-DailyCostVaucherReasonDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/daily-cost-vaucher-reason/update-DailyCostVaucherReason/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/daily-cost-vaucher-reason/save-DailyCostVaucherReason', model);
  }
  delete(id){
    return this.http.delete(this.baseUrl + '/daily-cost-vaucher-reason/delete-DailyCostVaucherReason/'+id);
  }

   getSelectedWarehousesList(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/warehouse/get-selectedWarehouses')
  }
  
}
