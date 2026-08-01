import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepositorService } from '../../service/Depositor.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-depositor',
  templateUrl: './new-depositor.component.html',
  styleUrls: ['./new-depositor.component.sass']
})
export class NewDepositorComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  DepositorForm: FormGroup;
  validationErrors: string[] = [];
  supplierList: SelectedModel[];
  pondList: SelectedModel[];
  warehouseList: SelectedModel[];
  unitList: SelectedModel[];
  paymentStatusList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private DepositorService: DepositorService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('depositorId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.DepositorService.find(+id).subscribe(
        res => {
          this.DepositorForm.patchValue({
            depositorId: res.depositorId,
            warehouseId: res.warehouseId,
            depositorName: res.depositorName,
            mobile: res.mobile,
            email: res.email,
            presentBalance: res.presentBalance,
            isActive: res.isActive

          });
        }
      );
    } else {
      this.pageTitle = 'New Stock Out ';
      this.destination = 'Add ';
      this.buttonText = "Save";
    }
    this.intitializeForm();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.DepositorForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.DepositorForm = this.fb.group({
      depositorId: [0],
      warehouseId: [],
      depositorName: [],
      mobile: [],
      email: [],
      presentBalance: [],
      isActive: [true],

    });
    

  }

  

  getWarehouseList() {
    this.DepositorService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  



  onSubmit() {
    const id = this.DepositorForm.get('depositorId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DepositorService.update(+id, this.DepositorForm.value).subscribe(response => {
            this.router.navigateByUrl('/capital-six/depositor-list');
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
      this.DepositorService.submit(this.DepositorForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/capital-six/depositor-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
