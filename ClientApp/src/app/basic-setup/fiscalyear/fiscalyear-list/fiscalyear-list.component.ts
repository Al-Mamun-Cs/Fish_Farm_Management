import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Fiscalyear} from '../../models/Fiscalyear';
import { FiscalyearService} from '../../service/Fiscalyear.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-fiscalyear-list',
  templateUrl: './fiscalyear-list.component.html',
  styleUrls: ['./fiscalyear-list.component.sass']
})
export class FiscalyearListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Fiscalyear[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','name', 'startDate','endDate', 'actions'];
  dataSource: MatTableDataSource<Fiscalyear> = new MatTableDataSource();

  selection = new SelectionModel<Fiscalyear>(true, []);

  
  constructor(private snackBar: MatSnackBar,private FiscalyearService:FiscalyearService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getFiscalyears();
  }
  
  getFiscalyears() {
    this.isLoading = true;
    this.FiscalyearService.getFiscalyears(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
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
    this.getFiscalyears();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getFiscalyears();
  } 
  deleteItem(row) {
    const id = row.fiscalyearId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.FiscalyearService.delete(id).subscribe(() => {
          this.getFiscalyears();
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
