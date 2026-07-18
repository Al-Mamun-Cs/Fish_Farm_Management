import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {BulletinService} from '../../service/Bulletin.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { Role } from 'src/app/core/models/role';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MasterData } from 'src/assets/data/master-data';
import { Bulletin } from '../../models/Bulletin'; 
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-new-bulletin',
  templateUrl: './new-bulletin.component.html',
  styleUrls: ['./new-bulletin.component.sass']
})
export class NewBulletinComponent implements OnInit {
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
  buttonText:string;
  userRole =Role;
  pageTitle: string;
  destination:string;
  BulletinForm: FormGroup;
  validationErrors: string[] = [];
  departmentName:SelectedModel[];
  
  role:any;
  branchId:any;

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private BulletinService: BulletinService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('bulletinId'); 

    this.role = this.authService.currentUserValue.role.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();


    if (id) {
      this.pageTitle = 'Edit Bulletin';
      this.destination='Edit';
      this.buttonText="Update";
      this.BulletinService.find(+id).subscribe(
        res => {
          this.BulletinForm.patchValue({ 
            bulletinId: res.bulletinId,
            buletinDetails: res.buletinDetails,
            status: res.status,
            startDate: res.startDate,
            endDate: res.endDate,
            menuPosition: res.menuPosition,
          
          });          
        }
      );
    } else {
      this.pageTitle = 'Create Bulletin';
      this.destination='Add';
      this.buttonText="Save";
    }
   
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      
    }
    
    this.getBulletins();
  }
  intitializeForm() {
    this.BulletinForm = this.fb.group({
      bulletinId: [0],
      buletinDetails: [''],
      status: [''],
      menuPosition: [''],
      isActive: [true],
      startDate:[''],
      endDate:['']
    })
  }

  
  pageChanged(event: PageEvent) {
    this.paging.pageIndex = event.pageIndex
    this.paging.pageSize = event.pageSize
    this.paging.pageIndex = this.paging.pageIndex + 1
    this.getBulletins();
  }

  getBulletins() {
    this.isLoading = true;
    this.BulletinService.getBulletins(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
     

      this.dataSource.data = response.items; 
      this.permission = response.permission;
      this.paging.length = response.totalItemsCount    
      this.isLoading = false;
    })
  }
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
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
  applyFilter(searchText){
    this.searchText = searchText;
    this.getBulletins();
  }
  onSubmit() {
    const id = this.BulletinForm.get('bulletinId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.BulletinService.update(+id,this.BulletinForm.value).subscribe(response => {
             this.router.navigateByUrl('/notification/bulletin-list');
            //this.reloadCurrentRoute();
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    }
    else {
      this.BulletinService.submit(this.BulletinForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/notification/bulletin-list');
        //this.reloadCurrentRoute();
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
