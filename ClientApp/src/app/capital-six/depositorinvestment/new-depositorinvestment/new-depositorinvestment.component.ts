import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DepositorInvestmentService } from '../../service/DepositorInvestment.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { WarehouseService } from '../../../basic-setup/service/Warehouse.service'



@Component({
  selector: 'app-new-depositorinvestment',
  templateUrl: './new-depositorinvestment.component.html',
  styleUrls: ['./new-depositorinvestment.component.sass']
})
export class NewDepositorInvestmentComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  DepositorInvestmentForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  depositorList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;
  cashInHand: string = '0';
  amountError: boolean = false;



  constructor(private snackBar: MatSnackBar, private authService: AuthService, private WarehouseService: WarehouseService, private datePipe: DatePipe, private confirmService: ConfirmService, private DepositorInvestmentService: DepositorInvestmentService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('depositorInvestmentId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.DepositorInvestmentService.find(+id).subscribe(
        res => {
          this.DepositorInvestmentForm.patchValue({
            depositorInvestmentId: res.depositorInvestmentId,
            warehouseId: res.warehouseId,
            depositorId: res.depositorId,
            investmenDate: res.investmenDate,
            investmenAmount: res.investmenAmount,
            principalReturn: res.principalReturn,
            profit: res.profit,
            businessOperatorName: res.businessOperatorName,
            mobile: res.mobile,
            address: res.address,
            remarks: res.remarks,
            closeStatus:res.closeStatus,
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
      this.DepositorInvestmentForm.get('warehouseId').setValue(this.branchId);
      this.getWarehouseData();
      this.getSelectedDepositorList();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.DepositorInvestmentForm = this.fb.group({
      depositorInvestmentId: [0],
      warehouseId: [],
      depositorId: [],
      investmenDate: [today],
      investmenAmount: [],
      principalReturn: [0],
      profit: [0],
      businessOperatorName: [''],
      mobile: [],
      address: [],
      remarks: [],
      closeStatus:[0],
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });
  }

  getWarehouseList() {
    this.DepositorInvestmentService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }
  getWarehouseData() {
    const warehouseId = this.DepositorInvestmentForm.get('warehouseId')?.value;

    this.WarehouseService.find(warehouseId).subscribe(res => {
      console.log(res, "Warehouse Data")
      this.cashInHand = String(res?.cashInHand ?? 0);
    });
  }

  getSelectedDepositorList() {
    const warehouseId = this.DepositorInvestmentForm.get('warehouseId').value;
    this.DepositorInvestmentService.getSelectedDepositorList(warehouseId).subscribe(res => {
      this.depositorList = res;
    });
  }

  onWarehouseChange() {
    this.getSelectedDepositorList();
    this.getWarehouseData();
  }
  validateInvestmenAmount() {
    const control = this.DepositorInvestmentForm.get('investmenAmount');
    const amount = Number(control?.value || 0);
    const cash = Number(this.cashInHand);

    this.amountError = amount > cash;

    if (this.amountError) {
      control?.setErrors({ maxAmount: true });
    } else {
      control?.setErrors(null);
    }
  }






  onSubmit() {
    const id = this.DepositorInvestmentForm.get('depositorInvestmentId').value;

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DepositorInvestmentService.update(+id, this.DepositorInvestmentForm.value).subscribe(response => {
            this.router.navigateByUrl('/capital-six/depositorinvestment-list');
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
      this.DepositorInvestmentService.submit(this.DepositorInvestmentForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/capital-six/depositorinvestment-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
