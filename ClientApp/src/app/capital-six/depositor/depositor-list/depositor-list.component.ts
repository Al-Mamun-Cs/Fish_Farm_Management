import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Depositor} from '../../models/Depositor';
import { DepositorService} from '../../service/Depositor.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-depositor-list',
  templateUrl: './depositor-list.component.html',
  styleUrls: ['./depositor-list.component.sass']
})
export class DepositorListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Depositor[] = [];
  isLoading = false;
  role:any;
  branchId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','useQty', 'actions'];
  dataSource: MatTableDataSource<Depositor> = new MatTableDataSource();

  selection = new SelectionModel<Depositor>(true, []);

  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private DepositorService:DepositorService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    this.getDepositors();
  }
  
  getDepositors() {
    this.isLoading = true;
    this.DepositorService.getDepositors(this.paging.pageIndex, this.paging.pageSize,this.searchText,this.branchId).subscribe(response => {
     
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
    this.getDepositors();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getDepositors();
  } 
  deleteItem(row) {
    const id = row.depositorId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.DepositorService.delete(id).subscribe(() => {
          this.getDepositors();
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
