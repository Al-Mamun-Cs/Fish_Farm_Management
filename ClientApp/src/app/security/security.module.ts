import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { SecurityRoutingModule } from './security-routing.module';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

import { RoleFeatureListComponent } from './rolefeature/rolefeature-list/rolefeature-list.component';
import { NewRoleFeatureComponent } from './rolefeature/new-rolefeature/new-rolefeature.component';
import { FeatureListComponent } from './feature/feature-list/feature-list.component';
import { NewFeatureComponent } from './feature/new-feature/new-feature.component';
import { ModuleListComponent } from './module/module-list/module-list.component';
import { NewModuleComponent } from './module/new-module/new-module.component';
import {NewRoleComponent} from './role/new-role/new-role.component';
import {RoleListComponent} from './role/role-list/role-list.component';
import { NewUserComponent } from './user/new-user/new-user.component';
import { UserListComponent } from './user/user-list/user-list.component';

import { MatRadioModule } from '@angular/material/radio';


import { RoleFeatureComponent } from './role/role-feature/role-feature.component';
import { DatabaseBackupComponent } from './databasebackup/new-databasebackup/new-databasebackup.component';
import { EmpUserListComponent } from './user/empuser-list/empuser-list.component';


@NgModule({
  declarations: [
   FeatureListComponent,
   NewFeatureComponent,
   ModuleListComponent,
   NewModuleComponent,
   NewRoleComponent,
   RoleListComponent,
   NewUserComponent,
   UserListComponent,
   RoleFeatureListComponent,
   NewRoleFeatureComponent,
   NewRoleFeatureComponent,
   RoleFeatureComponent,
   DatabaseBackupComponent,
   EmpUserListComponent,
  ],
  imports: [
    CommonModule,
    CommonModule,
    SecurityRoutingModule,
    CommonModule,
    FormsModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MaterialFileInputModule,
    MatRadioModule,
    MatAutocompleteModule
  ]
})
export class SecurityModule { }
