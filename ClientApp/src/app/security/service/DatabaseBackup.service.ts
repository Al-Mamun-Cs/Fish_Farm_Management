import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class DatabaseBackupService {
  private baseUrl = environment.securityUrl; 

  constructor(private http: HttpClient) { }

  triggerBackup(): Observable<any> { 
    return this.http.post<any>(`${this.baseUrl}/DatabaseBackup/backup-database`, {}).pipe(
      map((response: any) => {
        return response;
      })
    );
  }

}