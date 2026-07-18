import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Religion} from '../../models/Religion';
import { ReligionService } from '../../service/Religion.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';


@Component({
  selector: 'app-religion-list',
  templateUrl: './religion-list.component.html',
  styleUrls: ['./religion-list.component.sass']
})
export class ReligionListComponent implements OnInit {
  masterData = MasterData;
  ELEMENT_DATA: Religion[] = [];
  isLoading = false;
  showHideDiv:any;
  groupArrays: { warehouse: string; datas: any }[];
  pageTitle:any;
  role:any;
  branchId:any;
  supplierId:any;
  empolyeeId:any;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','bankInfo','supplier','warehouse','transactionDate','amount','remarks', 'actions'];
  dataSource: MatTableDataSource<Religion> = new MatTableDataSource();

  selection = new SelectionModel<Religion>(true, []);

  
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private ReligionService:ReligionService,private router: Router,private confirmService: ConfirmService,private route: ActivatedRoute) { }
  
  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    this.supplierId = this.authService.currentUserValue.supplierId.toString().trim();
    console.log(this.role, this.branchId,this.supplierId,"employee Id")
    this.getReligions();
  }
  
  getReligions() {
    this.isLoading = true;
    this.ReligionService.getReligions(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
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
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  
 
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getReligions();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getReligions();
  } 
  deleteItem(row) {
    const id = row.religionId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.ReligionService.delete(id).subscribe(() => {
          this.getReligions();
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
