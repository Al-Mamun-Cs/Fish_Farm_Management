import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FisheriesProductReturnService } from '../../service/FisheriesProductReturn.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { DatePipe } from '@angular/common';
import { AuthService } from 'src/app/core/service/auth.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { FisheriesInventoryService } from '../../service/FisheriesInventory.service'



@Component({
  selector: 'app-new-fisheriesproductreturn',
  templateUrl: './new-fisheriesproductreturn.component.html',
  styleUrls: ['./new-fisheriesproductreturn.component.sass']
})
export class NewFisheriesProductReturnComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  FisheriesProductReturnForm: FormGroup;
  validationErrors: string[] = [];
  warehouseList: SelectedModel[];
  productTypeList: SelectedModel[];
  productList: SelectedModel[];
  supplierList: SelectedModel[];
  role: any;
  branchId: any;
  fisheriesInventoryDetailId: any;
  options = [];
  filteredOptions;
  unitPurchasePrice: string = '0';
  availableQty: string = '0';
  amountError: boolean = false;



  constructor(private snackBar: MatSnackBar, private authService: AuthService, private FisheriesInventoryService: FisheriesInventoryService, private datePipe: DatePipe, private confirmService: ConfirmService, private FisheriesProductReturnService: FisheriesProductReturnService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)
    const id = this.route.snapshot.paramMap.get('fisheriesProductReturnId');
    if (id) {
      this.pageTitle = 'Stock Out Update ';
      this.destination = '';
      this.buttonText = "Update";
      this.FisheriesProductReturnService.find(+id).subscribe(
        res => {
          this.FisheriesProductReturnForm.patchValue({
            fisheriesProductReturnId: res.fisheriesProductReturnId,
            warehouseId: res.warehouseId,
            supplierId: res.supplierId,
            fisheriesProductTypeId: res.fisheriesProductTypeId,
            fisheriesInventoryDetailId: res.fisheriesInventoryDetailId,
            fisheriesInventoryId: res.fisheriesInventoryId,
            paymentReturnType: res.paymentReturnType,
            date: res.date,
            returnQty: res.returnQty,
            returnAmount: res.returnAmount,
            actualReturnValue: res.actualReturnValue,
            depreciationValue: res.depreciationValue,
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
      this.FisheriesProductReturnForm.get('warehouseId').setValue(this.branchId);
      this.onWarehouseChange();
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.FisheriesProductReturnForm = this.fb.group({
      fisheriesProductReturnId: [0],
      warehouseId: [],
      supplierId: [],
      date: [today],
      fisheriesProductTypeId: [],
      fisheriesInventoryDetailId: [],
      fisheriesInventoryId: [],
      paymentReturnType: [1],
      returnQty: [],
      returnAmount: [],
      actualReturnValue: [],
      depreciationValue: [],
      remarks: [],
      approveStatus: [0],
      approveBy: [],
      approveDate: [],
      isActive: [true],

    });
  }

  getWarehouseList() {
    this.FisheriesProductReturnService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }
  getSelectedSupplierList() {
    const warehouseId = this.FisheriesProductReturnForm.get('warehouseId').value;
    this.FisheriesProductReturnService.getSelectedSupplierList(warehouseId).subscribe(res => {
      this.supplierList = res;
    });
  }

  getSelectedProductTypeList() {
    const warehouseId = this.FisheriesProductReturnForm.get('warehouseId').value;
    this.FisheriesProductReturnService.getSelectedProductTypeList(warehouseId).subscribe(res => {
      this.productTypeList = res;
    });
  }

  getSelectedProduct() {
    const warehouseId = this.FisheriesProductReturnForm.get('warehouseId').value;
    const fisheriesProductTypeId = this.FisheriesProductReturnForm.get('fisheriesProductTypeId').value;
    this.FisheriesProductReturnService.getSelectedProduct(warehouseId, fisheriesProductTypeId).subscribe(res => {
      this.productList = res;
    });
  }
  getProductData() {
    const fisheriesInventoryDetailId = this.FisheriesProductReturnForm.get('fisheriesInventoryDetailId')?.value;
    this.FisheriesInventoryService.find(fisheriesInventoryDetailId).subscribe(res => {
      console.log(res, "Product Data")
      this.unitPurchasePrice = String(res?.unitPurchasePrice ?? 0);
      this.availableQty = String(res?.availableQty ?? 0);
      this.calculateActualReturnValue();
    });
  }
  checkReturnQty() {
    const returnQty = Number(this.FisheriesProductReturnForm.get('returnQty')?.value || 0);
    const availableQty = Number(this.availableQty || 0);
    this.amountError = false;
    if (returnQty > availableQty) {this.amountError = true;
      this.FisheriesProductReturnForm.get('returnQty')?.setValue(availableQty);
    }

    this.calculateActualReturnValue();
  }

  calculateActualReturnValue() {
    const returnQty = Number(this.FisheriesProductReturnForm.get('returnQty')?.value || 0);
    const unitPurchasePrice = Number(this.unitPurchasePrice || 0);
    const actualReturnValue = returnQty * unitPurchasePrice;
    this.FisheriesProductReturnForm.get('actualReturnValue')?.setValue( actualReturnValue.toFixed(2),
        { emitEvent: false }
      );
      this.calculateDepreciationValue();
  }
  
  calculateDepreciationValue() {
  const actualReturnValue = Number(
    this.FisheriesProductReturnForm
      .get('actualReturnValue')?.value || 0
  );
  const returnAmount = Number(
    this.FisheriesProductReturnForm
      .get('returnAmount')?.value || 0
  );
  const depreciationValue =
    actualReturnValue - returnAmount;
  this.FisheriesProductReturnForm
    .get('depreciationValue')
    ?.setValue(
      depreciationValue >= 0
        ? depreciationValue.toFixed(2)
        : '0.00',
      { emitEvent: false }
    );
}


  onWarehouseChange() {
    this.getSelectedProductTypeList();
    this.getSelectedSupplierList();
  }

  onPaymentReturnTypeChange(type: number) {
    this.FisheriesProductReturnForm.get('paymentReturnType').setValue(type);
    console.log('Payment Return Type:', this.FisheriesProductReturnForm.get('paymentReturnType').value
    );
  }



  onSubmit() {
    const id = this.FisheriesProductReturnForm.get('fisheriesProductReturnId').value;

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.FisheriesProductReturnService.update(+id, this.FisheriesProductReturnForm.value).subscribe(response => {
            this.router.navigateByUrl('/fishproduct-stock/fisheriesproductreturn-list');
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
      this.FisheriesProductReturnService.submit(this.FisheriesProductReturnForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/fishproduct-stock/fisheriesproductreturn-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}
