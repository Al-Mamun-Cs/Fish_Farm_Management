import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Pond } from '../../models/Pond';
import { PondService } from '../../service/Pond.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-pond-list',
  templateUrl: './pond-list.component.html',
  styleUrls: ['./pond-list.component.sass']
})
export class PondListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Pond[] = [];
  isLoading = false;
  window = window;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText = "";
  permission: any;
  displayedColumns: string[] = ['sl', 'nameBangla', 'nameEnglish', 'actions'];
  dataSource: MatTableDataSource<Pond> = new MatTableDataSource();

  selection = new SelectionModel<Pond>(true, []);


  constructor(private snackBar: MatSnackBar, private PondService: PondService, private router: Router, private confirmService: ConfirmService) { }

  ngOnInit() {
    this.getPonds();
  }

  getPonds() {
    this.isLoading = true;
    this.PondService.getPonds(this.paging.pageIndex, this.paging.pageSize, this.searchText).subscribe(response => {


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
    this.getPonds();
  }

  applyFilter(searchText: any) {
    this.searchText = searchText;
    this.getPonds();
  }
  deleteItem(row) {
    const id = row.pondId;
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) {
        this.PondService.delete(id).subscribe(() => {
          this.getPonds();
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
