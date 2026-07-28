import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyInvestorReturnService } from '../../service/CompanyInvestorReturn.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { CompanyInvestorService } from '../../service/CompanyInvestor.service';

@Component({
  selector: 'app-new-companyinvestorreturn',
  templateUrl: './new-companyinvestorreturn.component.html',
  styleUrls: ['./new-companyinvestorreturn.component.sass']
})
export class NewCompanyInvestorReturnComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  CompanyInvestorReturnForm: FormGroup;
  validationErrors: string[] = [];
  costReasonList: SelectedModel[];
  investorList: SelectedModel[];
  paymentStausList: SelectedModel[];
  warehouseList: SelectedModel[];
  reasonData: any;
  cashInHand: string = "0";
  totalDueAmount: number = 0;
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;
  investAmount: number = 0;
  returnInvestAmount: number = 0;
  amountError: string = '';

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private CompanyInvestorService: CompanyInvestorService, private datePipe: DatePipe, private confirmService: ConfirmService, private CompanyInvestorReturnService: CompanyInvestorReturnService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('companyInvestorReturnId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.CompanyInvestorReturnService.find(+id).subscribe(
        res => {
          this.CompanyInvestorReturnForm.patchValue({
            companyInvestorReturnId: res.companyInvestorReturnId,
            warehouseId: res.warehouseId,
            companyInvestorId: res.companyInvestorId,
            paymentStatusId: res.paymentStatusId,
            type: res.type,
            amount: res.amount,
            date: res.date,
            remarks: res.remarks,
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
    this.getSelectedPaymentStausList();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.CompanyInvestorReturnForm.get('warehouseId').setValue(this.branchId);
      this.getSelectedInvestorList();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.CompanyInvestorReturnForm = this.fb.group({
      companyInvestorReturnId: [0],
      warehouseId: [],
      companyInvestorId: [],
      paymentStatusId: [],
      type: [],
      amount: [],
      date: [today],
      remarks: [],
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });

  }

  getWarehouseList() {
    this.CompanyInvestorReturnService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }
  getSelectedPaymentStausList() {
    this.CompanyInvestorReturnService.getSelectedPaymentStausList().subscribe(res => {
      this.paymentStausList = res;
    });
  }
  getSelectedInvestorList() {
    const warehouseId = this.CompanyInvestorReturnForm.get('warehouseId')?.value;
    this.CompanyInvestorReturnService.getSelectedInvestorList(warehouseId).subscribe(res => {
      this.investorList = res;
      this.getInvestorData();
    });

  }

  getInvestorData() {
    const companyInvestorId = this.CompanyInvestorReturnForm.get('companyInvestorId')?.value;
    this.CompanyInvestorService.find(companyInvestorId).subscribe(res => {
      console.log(res, "Investor Data")
      this.investAmount = Number(res.investAmount) || 0;
      this.returnInvestAmount = Number(res.returnInvestAmount) || 0;
      console.log(this.investAmount);
      console.log(this.returnInvestAmount);
    });
  }
  get dueReturnAmount(): number {
    return this.investAmount - this.returnInvestAmount;
  }

  checkReturnAmount() {

  const type = Number(this.CompanyInvestorReturnForm.get('type')?.value);
  const amount = Number(this.CompanyInvestorReturnForm.get('amount')?.value);

  const amountControl = this.CompanyInvestorReturnForm.get('amount');

  this.amountError = '';

  if (type === 1 && amount > this.dueReturnAmount) {

    this.amountError = `বাকি মূলধন ${this.dueReturnAmount} টাকা। এর বেশি ফেরত দেওয়া যাবে না।`;

    amountControl?.setErrors({
      ...amountControl.errors,
      invalidAmount: true
    });

  } else {

    if (amountControl?.hasError('invalidAmount')) {

      const errors = { ...amountControl.errors };
      delete errors['invalidAmount'];

      amountControl.setErrors(Object.keys(errors).length ? errors : null);
    }
  }
}



  onSubmit() {
    const id = this.CompanyInvestorReturnForm.get('companyInvestorReturnId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.CompanyInvestorReturnService.update(+id, this.CompanyInvestorReturnForm.value).subscribe(response => {
            this.router.navigateByUrl('/financial-transactions/companyinvestorreturn-list');
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
      this.CompanyInvestorReturnService.submit(this.CompanyInvestorReturnForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/financial-transactions/companyinvestorreturn-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
