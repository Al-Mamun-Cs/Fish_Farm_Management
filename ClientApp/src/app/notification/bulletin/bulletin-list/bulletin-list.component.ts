import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {Bulletin} from '../../models/Bulletin';
import {BulletinService} from '../../service/Bulletin.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-bulletin-list',
  templateUrl: './bulletin-list.component.html',
  styleUrls: ['./bulletin-list.component.sass']
})
export class BulletinListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Bulletin[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','buletinDetails','startDate','endDate', 'actions'];
  dataSource: MatTableDataSource<Bulletin> = new MatTableDataSource();

  selection = new SelectionModel<Bulletin>(true, []);

  
  constructor(private snackBar: MatSnackBar,private BulletinService:BulletinService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getBulletins();
  }
  
  getBulletins() {
    this.isLoading = true;
    this.BulletinService.getBulletins(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
      console.log(this.dataSource.data,"Bulletin")
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
    this.getBulletins();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getBulletins();
  } 
  deleteItem(row) {
    const id = row.bulletinId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.BulletinService.delete(id).subscribe(() => {
          this.getBulletins();
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
