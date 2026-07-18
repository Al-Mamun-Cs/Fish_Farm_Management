import { SelectionModel } from '@angular/cdk/collections';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Role } from '../../models/role';
import { RoleService } from '../../service/role.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import {Location} from '@angular/common';

@Component({
  selector: 'app-role-feature',
  templateUrl: './role-feature.component.html',
  styleUrls: ['./role-feature.component.sass']
})
export class RoleFeatureComponent implements OnInit {

  masterData = MasterData;
  ELEMENT_DATA: Role[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  
  validationErrors: string[] = [];
  searchText="";
  roleId:string;
  dataList = [];
  displayedColumns: string[] = [ 'sl',/*'roleId',*/ 'roleName', 'loweredRoleName', /*'menuPosition',*/ 'actions'];
  dataSource: MatTableDataSource<Role> = new MatTableDataSource();
  permission: any;
  selection = new SelectionModel<Role>(true, []);

  
  constructor(private snackBar: MatSnackBar,private _location: Location,private route: ActivatedRoute,private roleService: RoleService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.roleId = this.route.snapshot.paramMap.get('roleId'); 
    console.log(this.roleId)
    this.getRoleFeatures(this.roleId);
  }
  getRoleFeatures(id) {
    // this.spinner.show();
    this.isLoading = true;
    this.roleService.getRoleFeatures(id).subscribe(response => {
      console.log(response)
      this.dataList = response;
    })
  }
  assignFeature(fieldName,event,roleFeature){
    // this.spinner.show();
    roleFeature[fieldName] = event.target.checked;
    console.log(this.roleId);
    console.log(roleFeature);
    // console.log(this.roleInfo);
    this.roleService.assignFeature(this.roleId,roleFeature).subscribe(response => {
      // this.router.navigateByUrl('/security/role-list');
      this.getRoleFeatures(this.roleId);
      this.snackBar.open('Information Updated Successfully ', '', {
        duration: 2000,
        verticalPosition: 'bottom',
        horizontalPosition: 'right',
        panelClass: 'snackbar-success'
      });
    }, error => {
      this.validationErrors = error;
    })
  }
  
  backClicked() {
    this._location.back();
  }
  getRoles() {
    this.isLoading = true;
    this.roleService.getRoles(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

      this.dataSource.data = response.items; 
      this.permission = response.permission; 
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.filteredData.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSource.filteredData.forEach((row) =>
          this.selection.select(row)
        );
  }
  addNew(){
    
  }
 
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getRoles();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getRoles();
  } 


  deleteItem(row) {
    const id = row.roleId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe(result => {
      console.log(result);
      if (result) {
        this.roleService.delete(id).subscribe(() => {
          this.getRoles();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });
        })
      }
    })
    
  }
}
