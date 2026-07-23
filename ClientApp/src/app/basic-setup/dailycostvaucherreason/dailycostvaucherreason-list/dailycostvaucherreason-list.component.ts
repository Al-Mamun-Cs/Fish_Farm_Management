import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { DailyCostVaucherReason } from '../../models/DailyCostVaucherReason';
import { DailyCostVaucherReasonService } from '../../service/DailyCostVaucherReason.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-dailycostvaucherreason-list',
  templateUrl: './dailycostvaucherreason-list.component.html',
  styleUrls: ['./dailycostvaucherreason-list.component.sass']
})
export class DailyCostVaucherReasonListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: DailyCostVaucherReason[] = [];
  isLoading = false;
  window = window;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  role:any;
  branchId:any;
  searchText = "";
  permission: any;
  displayedColumns: string[] = ['sl', 'nameBangla', 'nameEnglish', 'actions'];
  dataSource: MatTableDataSource<DailyCostVaucherReason> = new MatTableDataSource();

  selection = new SelectionModel<DailyCostVaucherReason>(true, []);


  constructor(private snackBar: MatSnackBar,private authService: AuthService, private DailyCostVaucherReasonService: DailyCostVaucherReasonService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getDailyCostVaucherReasons();
  }

  getDailyCostVaucherReasons() {
    this.isLoading = true;
    this.DailyCostVaucherReasonService.getDailyCostVaucherReasons(this.paging.pageIndex, this.paging.pageSize, this.searchText,this.branchId).subscribe(response => {


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
  addNew() {

  }

  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getDailyCostVaucherReasons();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getDailyCostVaucherReasons();
  }
  deleteItem(row) {
    const id = row.dailyCostVaucherReasonId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.DailyCostVaucherReasonService.delete(id).subscribe(() => {
          this.getDailyCostVaucherReasons();
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
