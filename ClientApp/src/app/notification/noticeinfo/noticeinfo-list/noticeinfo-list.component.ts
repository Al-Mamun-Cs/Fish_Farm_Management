import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import {NoticeInfo} from '../../models/NoticeInfo';
import {NoticeInfoService} from '../../service/NoticeInfo.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-noticeinfo-list',
  templateUrl: './noticeinfo-list.component.html',
  styleUrls: ['./noticeinfo-list.component.sass']
})
export class NoticeInfoListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: NoticeInfo[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','noticeTitle','noticeType','uploadPDFFile', 'actions'];
  dataSource: MatTableDataSource<NoticeInfo> = new MatTableDataSource();

  selection = new SelectionModel<NoticeInfo>(true, []);

  
  constructor(private snackBar: MatSnackBar,private NoticeInfoService:NoticeInfoService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getNoticeInfos();
  }
  
  getNoticeInfos() {
    this.isLoading = true;
    this.NoticeInfoService.getNoticeInfos(this.paging.pageIndex, this.paging.pageSize,this.searchText,"").subscribe(response => {
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
    this.getNoticeInfos();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getNoticeInfos();
  } 
  deleteItem(row) {
    const id = row.noticeInfoId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.NoticeInfoService.delete(id).subscribe(() => {
          this.getNoticeInfos();
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
