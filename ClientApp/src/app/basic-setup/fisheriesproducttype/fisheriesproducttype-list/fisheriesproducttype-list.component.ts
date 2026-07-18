import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {FisheriesProductType} from '../../models/FisheriesProductType';
import {FisheriesProductTypeService} from '../../service/FisheriesProductType.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-fisheriesproducttype-list',
  templateUrl: './fisheriesproducttype-list.component.html',
  styleUrls: ['./fisheriesproducttype-list.component.sass']
})
export class FisheriesProductTypeListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: FisheriesProductType[] = [];
  isLoading = false;
  window = window;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','nameBangla','nameEnglish', 'actions'];
  dataSource: MatTableDataSource<FisheriesProductType> = new MatTableDataSource();

  selection = new SelectionModel<FisheriesProductType>(true, []);

  
  constructor(private snackBar: MatSnackBar,private FisheriesProductTypeService:FisheriesProductTypeService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getFisheriesProductTypes();
  }
  
  getFisheriesProductTypes() {
    this.isLoading = true;
    this.FisheriesProductTypeService.getFisheriesProductTypes(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

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
    this.getFisheriesProductTypes();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getFisheriesProductTypes();
  } 
  deleteItem(row) {
    const id = row.fisheriesProductTypeId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.FisheriesProductTypeService.delete(id).subscribe(() => {
          this.getFisheriesProductTypes();
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
