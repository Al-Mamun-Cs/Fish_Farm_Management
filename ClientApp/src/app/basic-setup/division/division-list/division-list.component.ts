import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {Division} from '../../models/Division';
import {DivisionService} from '../../service/Division.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.sass']
})
export class DivisionListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Division[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','divisionName','nameBangla', 'actions'];
  dataSource: MatTableDataSource<Division> = new MatTableDataSource();

  selection = new SelectionModel<Division>(true, []);

  
  constructor(private snackBar: MatSnackBar,private DivisionService:DivisionService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getDivisions();
  }
  
  getDivisions() {
    this.isLoading = true;
    this.DivisionService.getDivisions(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
    console.log('API Response:', response); 
    console.log('Permission Object:', response.permission);
      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
        console.log('API Response:', response.permission); 
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
    this.getDivisions();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDivisions();
  } 
  deleteItem(row) {
    const id = row.divisionId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.DivisionService.delete(id).subscribe(() => {
          this.getDivisions();
          this.snackBar.open('Information Deleted Successfully ', '', {
            duration: 2000,
            verticalPosition: 'bottom',
            horizontalPosition: 'right',
            panelClass: 'snackbar-danger'
          });

        })
      }
      
    })
    
  }
}
