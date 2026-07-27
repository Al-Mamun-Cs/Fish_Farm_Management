import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyInvestorService } from '../../service/CompanyInvestor.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-companyinvestor',
  templateUrl: './new-companyinvestor.component.html',
  styleUrls: ['./new-companyinvestor.component.sass']
})
export class NewCompanyInvestorComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  CompanyInvestorForm: FormGroup;
  validationErrors: string[] = [];
  costReasonList: SelectedModel[];
  supplierCustomerList: SelectedModel[];
  paymentStausList: SelectedModel[];
  warehouseList: SelectedModel[];
  reasonData:any;
  cashInHand: string = "0";
  totalDueAmount: number = 0;
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private CompanyInvestorService: CompanyInvestorService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('companyInvestorId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.CompanyInvestorService.find(+id).subscribe(
        res => {
          this.CompanyInvestorForm.patchValue({
            companyInvestorId: res.companyInvestorId,
            warehouseId: res.warehouseId,
            fullName: res.fullName,
            shortName: res.shortName,
            phoneNo: res.phoneNo,
            email: res.email,
            date: res.date,
            investAmount: res.investAmount,
            returnInvestAmount: res.returnInvestAmount,
            profitAmount: res.profitAmount,
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
      this.CompanyInvestorForm.get('warehouseId').setValue(this.branchId);
      this.getWarehouseList();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.CompanyInvestorForm = this.fb.group({
      companyInvestorId: [0],
      warehouseId: [],
      fullName: [],
      shortName: [],
      phoneNo: [],
      email: [],
      date: [today],
      investAmount: [0],
      returnInvestAmount: [0],
      profitAmount: [0],
      isActive: [true],

    });
   
  }

  getWarehouseList() {
    this.CompanyInvestorService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  

  onSubmit() {
    const id = this.CompanyInvestorForm.get('companyInvestorId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.CompanyInvestorService.update(+id, this.CompanyInvestorForm.value).subscribe(response => {
            this.router.navigateByUrl('/financial-transactions/companyinvestor-list');
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
      this.CompanyInvestorService.submit(this.CompanyInvestorForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/financial-transactions/companyinvestor-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
