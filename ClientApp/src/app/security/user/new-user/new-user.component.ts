import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../service/User.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { BaseSchoolNameService } from '../../service/BaseSchoolName.service';
import {RoleService} from '../../service/role.service'
import { MasterData } from 'src/assets/data/master-data';
import { WarehouseService } from 'src/app/basic-setup/service/Warehouse.service';

@Component({
  selector: 'app-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.sass']
})
export class NewUserComponent implements OnInit {

  masterData= MasterData;
  pageTitle: string;
  destination:string;
  UserForm: FormGroup;
  buttonText:string;
  hide1 = true;
  hide2 = true;
  deptField = false;
  validationErrors: string[] = [];
  roleValues:SelectedModel[]; 
  branchValues:SelectedModel[]; 
  postValues:SelectedModel[]; 
  selectedOrganization:SelectedModel[];
  selectedCommendingArea:SelectedModel[];
  selectedBaseName:SelectedModel[];
  selectedSchoolName:SelectedModel[];
  organizationId:any;
  commendingAreaId:any;
  baseNameId:any;
  schoolNameId:any;
  isEdit:boolean=false;
  traineeId:any;

  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar,private RoleService: RoleService,private warehouseService: WarehouseService,private BaseSchoolNameService: BaseSchoolNameService,private confirmService: ConfirmService,private UserService: UserService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('userId'); 
    if (id) {
      this.pageTitle = 'Edit User';
      this.destination = "Role User";
      this.buttonText= "Update";
      this.isEdit=true;
      this.UserService.find(id).subscribe(
      
        res => {
          this.UserForm.patchValue({          

            id: res.id,
            userName: res.userName,
            roleName: res.roleName,
            password: 'Admin@123',
            confirmPassword: 'Admin@123',          
            firstName : res.firstName,
            lastName : res.lastName,
            phoneNumber : res.phoneNumber,
            branchId : res.branchId,
            supplierId:res.supplierId,
            departmentPostPositionId:res.departmentPostPositionId,
            traineeId:res.traineeId,
            photoPath: res.photoPath,
            signaturePath: res.signaturePath,
            email : res.email,          
            isActive: res.isActive
          });  
        }
      );
     

    } else {
      this.pageTitle = 'Create User';
      this.destination = "Role User";
      this.buttonText= "Save"
    }
    this.intitializeForm();
    this.getRoleName();
    this.getSelectedWarehousesList();
    //this.getSelectedPostPositionList();
  }

  intitializeForm() {
    this.UserForm = this.fb.group({
      id: [0],
      userName: ['', Validators.required],
      roleName: ['', Validators.required],
      password: ['Admin@123'],
      confirmPassword: ['Admin@123'],
      firstName: [''],
      lastName:[''],
      phoneNumber : ['', Validators.required],
      email : ['', Validators.required],
      branchId: [0],
      supplierId:[0],
      departmentPostPositionId:[0],
      traineeId:[0],
      name:[""],
      photoPath: [''],
      photo:[''],
      signaturePath: [''],
      signature:[''],
      isActive: [true],
  
     
    })
    
  }

 


  getRoleName(){
    this.RoleService.getselectedrole().subscribe(res=>{
        this.roleValues = res;
      console.log(this.roleValues);
    });
  }
 

  getSelectedWarehousesList(){
    this.warehouseService.getSelectedWarehousesList().subscribe(res=>{
        this.branchValues = res;
      console.log(this.branchValues);
    });
  }
  getSelectedPostPositionList(){
    let branchId = this.UserForm.get('branchId').value;
    this.warehouseService.getSelectedPostPositionList(branchId).subscribe(res=>{
        this.postValues = res;
      console.log(this.postValues);
    });
  }
  onPhotoFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('dddd')
    console.log(file);
      this.UserForm.patchValue({
        photo: file,
      });
    }
  }
    
  onSignatureFileChanged(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log('dddd')
    
      this.UserForm.patchValue({
        signature: file,
      });
      console.log(this.UserForm.value);
    }
  }
  onSubmit() {
    const id = this.UserForm.get('id').value;  
    const formData = new FormData();
    for (const key of Object.keys(this.UserForm.value)) {
      const value = this.UserForm.value[key];
      formData.append(key, value);
    }
    
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item').subscribe(result => {
        //console.log(result);
        if (result) {
          console.log(id);
          this.UserService.update(id,formData).subscribe(response => {
            this.router.navigateByUrl('/security/user-list');
            this.snackBar.open('User Updated Successfully ', '', {
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
    } else {
      this.UserService.submit(formData).subscribe(response => {
        this.router.navigateByUrl('/security/user-list');
        this.snackBar.open('User Created Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
