import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import {NoticeInfoService} from '../../service/NoticeInfo.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { MasterData } from 'src/assets/data/master-data';
import { MatTableDataSource } from '@angular/material/table';
import { NoticeInfo } from '../../models/NoticeInfo';
import { SelectionModel } from '@angular/cdk/collections';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Role } from 'src/app/core/models/role';

@Component({
  selector: 'app-new-noticeinfo',
  templateUrl: './new-noticeinfo.component.html',
  styleUrls: ['./new-noticeinfo.component.sass']
})
export class NewNoticeInfoComponent implements OnInit {
  masterData = MasterData;
  buttonText:string;
  pageTitle: string;
  destination:string;
  NoticeInfoForm: FormGroup;
  validationErrors: string[] = [];
  isLoading = false;
  role:any;
  userRole=Role;
  branchId:any;
  baseSchoolId:any;
  selectedNoticeType:SelectedModel[];
  selectedMemberName:SelectedModel[];
  departmentName:SelectedModel[];
  itemValue: string;
  options = [];
  filteredOptions;
  bookTitleInfoId:number;
  issuableToggle:string;
  bookInformationId: number;
  illustrationsToggle:string;
  getmemberinfoid: number;
  memberInfoId:any;
  getmembername: string;
  bookTitleEnglish:any;
  bookTitleBangla:any;
  showMemberAutoComplete=true;
  isTableShow=false;
  noticeTypeIdValue:any;
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
  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private NoticeInfoService: NoticeInfoService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('noticeInfoId'); 

    this.role = this.authService.currentUserValue.role.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();
    console.log(this.role,"-"+ this.branchId)

    if (id) {
      this.pageTitle = 'Edit Notice Info';
      this.destination='Edit';
      this.buttonText="Update";
      this.NoticeInfoService.find(+id).subscribe(
        res => {
          this.NoticeInfoForm.patchValue({          

            noticeInfoId: res.noticeInfoId,
            baseSchoolNameId: res.baseSchoolNameId,
            noticeTypeId:res.noticeTypeId,
            noticeTitle:res.noticeTitle,
            uploadPDFFile:res.uploadPDFFile,
            remarks:res.remarks,
            viewStatus:res.viewStatus,
            detailViewStatus:res.detailViewStatus,
            //menuPosition:res.menuPosition,
           
          
          });   

          //this.bookInformationId=res.bookInformationId;
        }
      );
    } else {
      this.pageTitle = 'Create Notice Info';
      this.destination='Add';
      this.buttonText="Save";
    }
    this.intitializeForm();
    if(this.role != this.userRole.SuperAdmin){
      this.NoticeInfoForm.get('baseSchoolNameId').setValue(this.branchId);
      
    }
    this.GetDepartmentNameById(this.masterData.schoolDept.issakhanLMS);
    this.getselectedNoticeType();
   // this.getNoticeInfos();
  }
  intitializeForm() {
    this.NoticeInfoForm = this.fb.group({
      noticeInfoId: [0],
      baseSchoolNameId: [''],
      noticeTypeId:[''],
      noticeTitle:[''],
      uploadPDFFile:[''],
      doc:[''],
      pno:[''],
      remarks: [''],
      noticeEndDate:[],
      viewStatus:[0],
      detailViewStatus:[0],
      //menuPosition: [''],
      isActive: [true],
      memberInfoId:['']
    })
    //autocomplete for PNO
    this.NoticeInfoForm.get('pno').valueChanges
    .subscribe(value => {
        this.getSelectedPno(value);
    })
  }

  //autocomplete for PNO
onPnoSelectionChanged(item) {
  this.memberInfoId = item.value
  this.NoticeInfoForm.get('memberInfoId').setValue(item.value);
  this.NoticeInfoForm.get('pno').setValue(item.text);
   console.log(item.value)
}
onNoticeTypeSelectionChange(noticeTypeId){
  console.log(event);
  if(noticeTypeId == 1002){
    this.NoticeInfoForm.get('memberInfoId').setValue(0);
    this.showMemberAutoComplete=false;
    //this.isTableShow=true;
    this.noticeTypeIdValue= noticeTypeId;
    this.getNoticeInfos(0);
  }
  else{
    this.showMemberAutoComplete=true;
    this.noticeTypeIdValue= noticeTypeId;
    this.getNoticeInfos(this.noticeTypeIdValue);
   // this.isTableShow =false;
  }
}
  //autocomplete for PNO
  getSelectedPno(pno){
  this.NoticeInfoService.getSelectedPno(pno).subscribe(response => {
    this.options = response;
    this.filteredOptions = response;
  })
}

applyFilter(searchText: any){ 
  this.searchText = searchText;
  this.getNoticeInfos(this.noticeTypeIdValue);
} 
deleteItem(row) {
  const id = row.noticeInfoId; 
  this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This  Item?').subscribe(result => {
    console.log(result);
    if (result) { 
      this.NoticeInfoService.delete(id).subscribe(() => {
        this.getNoticeInfos(this.noticeTypeIdValue);
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
getNoticeInfos(noticeTypeId) {
  this.isLoading = true;
  this.NoticeInfoService.getNoticeInfos(this.paging.pageIndex, this.paging.pageSize,this.searchText,noticeTypeId).subscribe(response => {
   

    this.dataSource.data = response.items; 
    this.permission = response.permission;
    this.paging.length = response.totalItemsCount    
    this.isLoading = false;
  })
}

  GetDepartmentNameById(baseNameId){    
    this.NoticeInfoService.getSelectedSchoolName(baseNameId).subscribe(res=>{
      this.departmentName=res
      console.log("departmentName")
      console.log(this.departmentName)
    }); 
  }
  getselectedNoticeType(){
    this.NoticeInfoService.getselectedNoticeType().subscribe(res=>{
      this.selectedNoticeType=res
    });
  }
  onFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('dddd')
     console.log(file);
      this.NoticeInfoForm.patchValue({
        doc: file,
      });
    }
  }
  
  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
  }
  
  onSubmit() {
    const id = this.NoticeInfoForm.get('noticeInfoId').value;  
    this.NoticeInfoForm.get("noticeEndDate").setValue(new Date(this.NoticeInfoForm.get("noticeEndDate").value).toUTCString()
    ); 
    //this.NoticeInfoForm.get('baseSchoolNameId').setValue(this.branchId);
    console.log(this.NoticeInfoForm.value)
    const formData = new FormData();
    for (const key of Object.keys(this.NoticeInfoForm.value)) {
      const value = this.NoticeInfoForm.value[key];
      formData.append(key, value);
    }
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.NoticeInfoService.update(+id,formData).subscribe(response => {
            this.router.navigateByUrl('/notification/noticeinfo-list');
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
      this.NoticeInfoService.submit(formData).subscribe(response => {
        //this.reloadCurrentRoute();
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/notification/noticeinfo-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }
  

}
