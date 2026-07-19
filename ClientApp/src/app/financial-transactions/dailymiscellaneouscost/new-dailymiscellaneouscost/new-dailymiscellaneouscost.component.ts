import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DailyMiscellaneousCostService } from '../../service/DailyMiscellaneousCost.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-dailymiscellaneouscost',
  templateUrl: './new-dailymiscellaneouscost.component.html',
  styleUrls: ['./new-dailymiscellaneouscost.component.sass']
})
export class NewDailyMiscellaneousCostComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  DailyMiscellaneousCostForm: FormGroup;
  validationErrors: string[] = [];
  costReasonList: SelectedModel[];
  paymentStausList: SelectedModel[];
  warehouseList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private DailyMiscellaneousCostService: DailyMiscellaneousCostService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('dailyMiscellaneousCostId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.DailyMiscellaneousCostService.find(+id).subscribe(
        res => {
          this.DailyMiscellaneousCostForm.patchValue({
            dailyMiscellaneousCostId: res.dailyMiscellaneousCostId,
            warehouseId: res.warehouseId,
            dailyCostVaucherReasonId: res.dailyCostVaucherReasonId,
            empolyeeId: res.empolyeeId,
            paymentStatusId: res.paymentStatusId,
            transactionDate: res.transactionDate,
            amount: res.amount,
            remarks: res.remarks,
            approvedStatus: res.approvedStatus,
            approvedBy: res.approvedBy,
            approvedDate: res.approvedDate,
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
    this.getSelectedPaymentStausList();
    this.getSelectedDailyCostReasonsList();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.DailyMiscellaneousCostForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.DailyMiscellaneousCostForm = this.fb.group({
      dailyMiscellaneousCostId: [0],
      warehouseId: [],
      dailyCostVaucherReasonId: [],
      empolyeeId: [],
      paymentStatusId: [1],
      amount: [],
      transactionDate: [today],
      remarks: [''],
      approvedStatus: [0],
      approvedBy: [],
      approvedDate: [],
      isActive: [true],

    });
   
  }

  getWarehouseList() {
    this.DailyMiscellaneousCostService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedPaymentStausList() {
    this.DailyMiscellaneousCostService.getSelectedPaymentStausList().subscribe(res => {
      this.paymentStausList = res;
    });
  }
  getSelectedDailyCostReasonsList() {
    this.DailyMiscellaneousCostService.getSelectedDailyCostReasonsList(this.branchId).subscribe(res => {
      this.costReasonList = res;
    });
  }


  onSubmit() {
    const id = this.DailyMiscellaneousCostForm.get('dailyMiscellaneousCostId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.DailyMiscellaneousCostService.update(+id, this.DailyMiscellaneousCostForm.value).subscribe(response => {
            this.router.navigateByUrl('/financial-transactions/dailymiscellaneouscost-list');
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
      this.DailyMiscellaneousCostService.submit(this.DailyMiscellaneousCostForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/financial-transactions/dailymiscellaneouscost-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
