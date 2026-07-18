import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {FisheriesUnit} from '../../models/FisheriesUnit';
import {FisheriesUnitService} from '../../service/FisheriesUnit.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-fisheriesunit-list',
  templateUrl: './fisheriesunit-list.component.html',
  styleUrls: ['./fisheriesunit-list.component.sass']
})
export class FisheriesUnitListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: FisheriesUnit[] = [];
  isLoading = false;
  window = window;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','fullName','shortName', 'actions'];
  dataSource: MatTableDataSource<FisheriesUnit> = new MatTableDataSource();

  selection = new SelectionModel<FisheriesUnit>(true, []);

  
  constructor(private snackBar: MatSnackBar,private FisheriesUnitService:FisheriesUnitService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getFisheriesUnits();
  }
  
  getFisheriesUnits() {
    this.isLoading = true;
    this.FisheriesUnitService.getFisheriesUnits(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

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
    this.getFisheriesUnits();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getFisheriesUnits();
  } 
  deleteItem(row) {
    const id = row.fisheriesUnitId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.FisheriesUnitService.delete(id).subscribe(() => {
          this.getFisheriesUnits();
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
