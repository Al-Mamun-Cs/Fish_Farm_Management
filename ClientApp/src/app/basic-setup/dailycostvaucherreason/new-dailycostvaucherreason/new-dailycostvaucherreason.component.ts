import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import {DailyCostVaucherReasonService} from '../../service/DailyCostVaucherReason.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-new-dailycostvaucherreason',
  templateUrl: './new-dailycostvaucherreason.component.html',
  styleUrls: ['./new-dailycostvaucherreason.component.sass']
})
export class NewDailyCostVaucherReasonComponent implements OnInit {
  buttonText:string;
  pageTitle: string;
  destination:string;
  DailyCostVaucherReasonForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];

  role: any;
  branchId: any;

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private DailyCostVaucherReasonService: DailyCostVaucherReasonService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('dailyCostVaucherReasonId'); 
    if (id) {
      this.pageTitle = 'Product Type Update ';
      this.destination='Update';
      this.buttonText="Update";
      this.DailyCostVaucherReasonService.find(+id).subscribe(
        res => {
          this.DailyCostVaucherReasonForm.patchValue({          

            dailyCostVaucherReasonId: res.dailyCostVaucherReasonId,
            warehouseId: res.warehouseId,
            fullName: res.fullName,
            shortName: res.shortName,
            menuPosition:res.menuPosition,
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
      this.DailyCostVaucherReasonForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    this.DailyCostVaucherReasonForm = this.fb.group({
      dailyCostVaucherReasonId: [0],
      warehouseId: [],
      fullName: [''],
      shortName: [],
      menuPosition:[],
      isActive: [true],
     
    })
  }
  getWarehouseList() {
    this.DailyCostVaucherReasonService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }
  
  onSubmit() {
    const id = this.DailyCostVaucherReasonForm.get('dailyCostVaucherReasonId').value;   
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DailyCostVaucherReasonService.update(+id,this.DailyCostVaucherReasonForm.value).subscribe(response => {
            this.router.navigateByUrl('/basic-setup/dailycostvaucherreason-list');
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
      this.DailyCostVaucherReasonService.submit(this.DailyCostVaucherReasonForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/basic-setup/dailycostvaucherreason-list');
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }

}
