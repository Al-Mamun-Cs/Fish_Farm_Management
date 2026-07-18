import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {FisheriesProductTypeService} from '../../service/FisheriesProductType.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';

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

  constructor(private snackBar: MatSnackBar,private confirmService: ConfirmService,private FisheriesProductTypeService: FisheriesProductTypeService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('fisheriesProductTypeId'); 
    if (id) {
      this.pageTitle = 'Product Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.FisheriesProductTypeService.find(+id).subscribe(
        res => {
          this.FisheriesProductTypeForm.patchValue({          

            fisheriesProductTypeId: res.fisheriesProductTypeId,
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
  }
  intitializeForm() {
    this.FisheriesProductTypeForm = this.fb.group({
      fisheriesProductTypeId: [0],
      nameEnglish: [''],
      nameBangla: [],
      isActive: [true],
     
    })
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
