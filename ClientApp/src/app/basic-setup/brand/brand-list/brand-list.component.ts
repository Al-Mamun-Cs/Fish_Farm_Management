import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Brand} from '../../models/Brand';
import { BrandService} from '../../service/Brand.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Router } from '@angular/router';
import { MasterData } from 'src/assets/data/master-data';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.sass']
})
export class BrandListComponent implements OnInit {
  photoBaseUrl = environment.fileUrl;
  masterData = MasterData;
  ELEMENT_DATA: Brand[] = [];
  isLoading = false;

  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  }
  searchText="";
  permission: any;
  displayedColumns: string[] = [ 'sl','fullName', 'shortName','brandImages', 'actions'];
  dataSource: MatTableDataSource<Brand> = new MatTableDataSource();

  selection = new SelectionModel<Brand>(true, []);

  
  constructor(private snackBar: MatSnackBar,private BrandService:BrandService,private router: Router,private confirmService: ConfirmService) { }
  
  ngOnInit() {
    this.getBrands();
  }
  
  getBrands() {
    this.isLoading = true;
    this.BrandService.getBrands(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     
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
 getEmployeeImage(path: string): string {
  if (!path) return 'assets/no-image.png'; // fallback
  return this.photoBaseUrl + path;
}
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getBrands();
  }

  applyFilter(searchText: any){ 
    this.searchText = searchText;
    this.getBrands();
  } 
  deleteItem(row) {
    const id = row.brandId; 
    this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
      console.log(result);
      if (result) { 
        this.BrandService.delete(id).subscribe(() => {
          this.getBrands();
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
