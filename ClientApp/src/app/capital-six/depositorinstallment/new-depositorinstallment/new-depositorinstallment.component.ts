import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepositorInstallmentService } from '../../service/DepositorInstallment.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-depositorinstallment',
  templateUrl: './new-depositorinstallment.component.html',
  styleUrls: ['./new-depositorinstallment.component.sass']
})
export class NewDepositorInstallmentComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  DepositorInstallmentForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  depositorList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private DepositorInstallmentService: DepositorInstallmentService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('depositorInstallmentId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.DepositorInstallmentService.find(+id).subscribe(
        res => {
          this.DepositorInstallmentForm.patchValue({
            depositorInstallmentId: res.depositorInstallmentId,
            warehouseId: res.warehouseId,
            depositorId:res.depositorId,
            installmentDate: res.installmentDate,
            installmentAmount: res.installmentAmount,
            image: res.image,
            approveStatus: res.approveStatus,
            approveBy: res.approveBy,
            approveDate: res.approveDate,
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
      this.DepositorInstallmentForm.get('warehouseId').setValue(this.branchId);
      this.getSelectedDepositorList();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.DepositorInstallmentForm = this.fb.group({
      depositorInstallmentId: [0],
      warehouseId: [],
      depositorId:[],
      installmentDate: [today],
      installmentAmount: [],
      image: [],
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });
    

  }

  

  getWarehouseList() {
    this.DepositorInstallmentService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedDepositorList() {
     const warehouseId = this.DepositorInstallmentForm.get('warehouseId').value;
    this.DepositorInstallmentService.getSelectedDepositorList(warehouseId).subscribe(res => {
      this.depositorList = res;
    });
  }

  



  onSubmit() {
    const id = this.DepositorInstallmentForm.get('depositorInstallmentId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DepositorInstallmentService.update(+id, this.DepositorInstallmentForm.value).subscribe(response => {
            this.router.navigateByUrl('/capital-six/depositorinstallment-list');
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
      this.DepositorInstallmentService.submit(this.DepositorInstallmentForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/capital-six/depositorinstallment-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
