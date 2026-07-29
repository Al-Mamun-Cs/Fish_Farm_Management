import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FishSaleService } from '../../service/FishSale.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-new-fishsale',
  templateUrl: './new-fishsale.component.html',
  styleUrls: ['./new-fishsale.component.sass']
})
export class NewFishSaleComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  FishSaleForm: FormGroup;
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

  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private confirmService: ConfirmService, private FishSaleService: FishSaleService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('fishSaleId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.FishSaleService.find(+id).subscribe(
        res => {
          this.FishSaleForm.patchValue({
            fishSaleId: res.fishSaleId,
            warehouseId: res.warehouseId,
            pondId: res.pondId,
            supplierId: res.supplierId,
            fisheriesUnitId: res.fisheriesUnitId,
            saleDate: res.saleDate,
            paymentStatusId: res.paymentStatusId,
            saleQty: res.saleQty,
            unitSalePrice: res.unitSalePrice,
            totalSalePrice: res.totalSalePrice,
            salePaidAmount: res.salePaidAmount,
            saleDueAmount: res.saleDueAmount,
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
    this.getSelectedPondList();
    this.getSelectedUnitList();
    this.getSelectedPaymentStausList();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.FishSaleForm.get('warehouseId').setValue(this.branchId);
      this.getSelectedSupplierList();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.FishSaleForm = this.fb.group({
      fishSaleId: [0],
      warehouseId: [],
      pondId: [],
      supplierId: [],
      fisheriesUnitId: [],
      paymentStatusId: [],
      saleDate: [today],
      saleQty: [],
      unitSalePrice: [],
      totalSalePrice: [0],
      salePaidAmount: [],
      saleDueAmount: [0],
      isActive: [true],

    });
    this.FishSaleForm.get('saleQty')?.valueChanges.subscribe(() => {
      this.calculateSale();
    });

    this.FishSaleForm.get('unitSalePrice')?.valueChanges.subscribe(() => {
      this.calculateSale();
    });

    this.FishSaleForm.get('salePaidAmount')?.valueChanges.subscribe(() => {
      this.calculateDueAmount();
    });
    this.FishSaleForm.get('supplierId')?.valueChanges.subscribe(() => {
      this.validateCustomerSelection();
    });

  }

  calculateSale() {

    const qty = Number(this.FishSaleForm.get('saleQty')?.value) || 0;
    const price = Number(this.FishSaleForm.get('unitSalePrice')?.value) || 0;

    const total = qty * price;

    this.FishSaleForm.patchValue({
      totalSalePrice: total
    }, { emitEvent: false });
    this.calculateDueAmount();
  }

  calculateDueAmount() {
    const total = Number(this.FishSaleForm.get('totalSalePrice')?.value) || 0;
    let paid = Number(this.FishSaleForm.get('salePaidAmount')?.value) || 0;
    if (paid > total) {
      this.snackBar.open(
        `পরিশোধিত অর্থের পরিমাণ মোট বিক্রয়মূল্যের চেয়ে বেশি হতে পারবে না (${total})`,
        '',
        {
          duration: 4000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          panelClass: 'snackbar-danger'
        }
      );
      paid = total;
      this.FishSaleForm.patchValue({
        salePaidAmount: total
      }, { emitEvent: false });
    }

    const due = total - paid;

    this.FishSaleForm.patchValue({
      saleDueAmount: due
    }, { emitEvent: false });
    this.validateCustomerSelection();

  }
  validateCustomerSelection() {

    const supplierId = this.FishSaleForm.get('supplierId')?.value;
    const dueControl = this.FishSaleForm.get('saleDueAmount');

    const due = Number(dueControl?.value) || 0;

    if (due > 0 && (!supplierId || supplierId == 0)) {

      dueControl?.setErrors({
        ...(dueControl.errors || {}),
        customerRequired: true
      });

      dueControl?.markAsTouched();

    } else {

      if (dueControl?.errors) {
        delete dueControl.errors['customerRequired'];

        if (Object.keys(dueControl.errors).length === 0) {
          dueControl.setErrors(null);
        } else {
          dueControl.setErrors(dueControl.errors);
        }
      }
    }
  }

  getWarehouseList() {
    this.FishSaleService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getSelectedPondList() {
    this.FishSaleService.getSelectedPondList().subscribe(res => {
      this.pondList = res;
    });
  }
  getSelectedSupplierList() {
    const warehouseId = this.FishSaleForm.get('warehouseId')?.value;
    this.FishSaleService.getSelectedSupplierList(warehouseId).subscribe(res => {
      this.supplierList = res;
    });
  }
  getSelectedUnitList() {
    this.FishSaleService.getSelectedUnitList().subscribe(res => {
      this.unitList = res;
    });
  }
  getSelectedPaymentStausList() {
    this.FishSaleService.getSelectedPaymentStausList().subscribe(res => {
      this.paymentStatusList = res;
    });
  }



  onSubmit() {
    const id = this.FishSaleForm.get('fishSaleId').value;
    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FishSaleService.update(+id, this.FishSaleForm.value).subscribe(response => {
            this.router.navigateByUrl('/stock-consumption/fishsale-list');
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
      this.FishSaleService.submit(this.FishSaleForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/stock-consumption/fishsale-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
