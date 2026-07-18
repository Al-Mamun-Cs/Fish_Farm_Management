import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { SelectedModel } from "src/app/core/models/selectedModel";
import { map } from "rxjs";
@Injectable({
  providedIn: "root",
})
export class ReportService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}
  
  
  GetSpEasyBikeRenewalListByTypeAndDateRange(renewalStatus, bankId, fromDate, toDate){
    return this.http.get<any[]>(this.baseUrl + '/easy-bike-renewals/get-spEasyBikeRenewalListByTypeAndDateRange?renewalStatus='+renewalStatus+'&bankId='+bankId+'&fromDate='+fromDate+'&toDate='+toDate)
  }
  
  GetSpEasyBikeInfoListByTypeAndWordNo(wordNoId,status,typeId){
    return this.http.get<any[]>(this.baseUrl + '/easy-bike-information/get-spEasyBikeInfoListByTypeAndWordNo?wordNoId='+wordNoId+'&status='+status+'&typeId='+typeId)
  }

}
