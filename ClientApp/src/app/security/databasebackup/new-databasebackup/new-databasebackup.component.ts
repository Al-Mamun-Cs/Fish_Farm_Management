

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { DatabaseBackupService } from '../../service/DatabaseBackup.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-new-databasebackup',
  templateUrl: './new-databasebackup.component.html',
  styleUrls: ['./new-databasebackup.component.sass']
})
export class DatabaseBackupComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  SalaryForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList:SelectedModel[];
  employeeList:SelectedModel[];
  role:any;
  branchId:any;

   backupStatus: string = '';
  backupMessage: string = '';
  isLoading: boolean = false;


  constructor(private snackBar: MatSnackBar, private datePipe: DatePipe,private authService: AuthService,private confirmService: ConfirmService,private DatabaseBackupService: DatabaseBackupService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {

  }

  triggerDatabaseBackup(): void {
    this.isLoading = true;
    this.backupStatus = 'In progress...';
    this.backupMessage = '';

    this.DatabaseBackupService.triggerBackup().subscribe({
      next: (response) => {
        this.isLoading = false;
        this.backupStatus = response.success ? 'Success' : 'Failed';
        this.backupMessage = response.message;
      },
      error: (error: HttpErrorResponse) => {
        this.isLoading = false;
        this.backupStatus = 'Failed';
        if (error.error && error.error.message) {
          this.backupMessage = error.error.message;
        } else {
          this.backupMessage = 'An unknown error occurred during backup.';
        }
        console.error('Backup Error:', error);
      }
    });
  }
  
  

}
