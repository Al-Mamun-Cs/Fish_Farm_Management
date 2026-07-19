import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {FisheriesProductTypeService} from '../../service/FisheriesProductType.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-fisheriesproducttype',
  templateUrl: './new-fisheriesproducttype.component.html',
  styleUrls: ['./new-fisheriesproducttype.component.sass']
})
export class NewFisheriesProductTypeComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  FisheriesProductTypeForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];

  role: any;
  branchId: any;

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private FisheriesProductTypeService: FisheriesProductTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('fisheriesProductTypeId'); 
    if (id) {
      this.pageTitle = 'Product Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.FisheriesProductTypeService.find(+id).subscribe(
        res => {
          this.FisheriesProductTypeForm.patchValue({          

            fisheriesProductTypeId: res.fisheriesProductTypeId,
            warehouseId: res.warehouseId,
            nameEnglish: res.nameEnglish,
            nameBangla: res.nameBangla,
            isActive: res.isActive
          
          });          
        }
      );
    } else {
      this.pageTitle = 'New Product Type ';
      this.destination='Add ';
      this.buttonText="Save";
    }
    this.intitializeForm();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.FisheriesProductTypeForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    this.FisheriesProductTypeForm = this.fb.group({
      fisheriesProductTypeId: [0],
      warehouseId: [],
      nameEnglish: [''],
      nameBangla: [],
      isActive: [true],
     
    })
  }
  getWarehouseList() {
    this.FisheriesProductTypeService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }
  
  onSubmit() {
    const id = this.FisheriesProductTypeForm.get('fisheriesProductTypeId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FisheriesProductTypeService.update(+id,this.FisheriesProductTypeForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/fisheriesproducttype-list');
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
      this.FisheriesProductTypeService.submit(this.FisheriesProductTypeForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/fisheriesproducttype-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
