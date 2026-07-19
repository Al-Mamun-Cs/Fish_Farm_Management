import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopInventoryService } from '../../service/ShopInventory.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { SupplierService } from '../../../basic-setup/service/Supplier.service';
import { HostListener } from '@angular/core';

@Component({
  selector: 'app-new-shopinventory',
  templateUrl: './new-shopinventory.component.html',
  styleUrls: ['./new-shopinventory.component.sass']
})
export class NewShopInventoryComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  InventoryForm: FormGroup;
  validationErrors: string[] = [];
  unitList: SelectedModel[];
  productTypeList: SelectedModel[];
  paymentStatusList: SelectedModel[];
  supplierList: SelectedModel[];
  inventoryList: SelectedModel[];
  warehouseList: SelectedModel[];
  unitHide: boolean = false;
  unitPaymentType: boolean = false;
  showWeightingScaleNo = false;
  feesAmount: any;
  costPrice: any;
  productTypeId: any;
  salelessAmount: number = 0;
  transportCost: number = 0;
  total: number = 0;
  productTypeListForRow: any[][] = [];
  role: any;
  branchId: any;
  totalAdvanceAmount: any;
  totalDueAmount: any;
  totalPaidAmount: any;
  supplierId: any
  categoryId: any;

  options = [];
  filteredOptions;
  screenWidth = window.innerWidth;


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private SupplierService: SupplierService, private confirmService: ConfirmService, private ShopInventoryService: ShopInventoryService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)

    const id = this.route.snapshot.paramMap.get('shopInventoryId');

    {
      this.pageTitle = 'New Purchase';
      this.destination = 'Add ';
      this.buttonText = "Save";
    }
    this.intitializeForm();
    this.generateBillNo();
    this.getPaymentStatusList();
    this.getSelectedProductTypeList();
    this.getSelectedUnitList();
    this.getWarehouseList();
    if (this.branchId > 0) {
      this.InventoryForm.get('warehouseId').setValue(this.branchId);
    }
  }
  intitializeForm() {
    const today = this.datePipe.transform(new Date(), 'dd-MMM-yyyy');
    this.InventoryForm = this.fb.group({
      shopInventoryId: [0],
      warehouseId: [],
      supplierId: [],
      supplierName: [""],
      paymentStatusId: [],
      voucherNo: [''],
      purchaseDate: [today],
      lessAmount: [0],
      transportCost: [0],
      totalPurchasePrice: [0],
      paidAmount: [],
      dueAmount: [0],
      approveStatus: [false],
      isActive: [true],
      shopInventoryDetail: this.fb.array([
        this.createProductDetailsForm()
      ]),

    });
    //autocomplete for supplierName
    this.InventoryForm.get('supplierName').valueChanges
      .subscribe(value => {
        this.getSelectedSupplierName(value);
      })


  }
  private createProductDetailsForm() {
    return this.fb.group({
      fisheriesProductTypeId: [0],
      fisheriesUnitId: [0],
      productName: [''],
      lessAmount: [0],
      transportCost: [],
      costingPrice: [],
      totalUnitQty: [''],
      unitPurchasePrice: [''],
      totalUnitPurchasePrice: [''],
      availableQty: [],
      damageQty: [0],
      returnQty: [0],
    });
  }
  //autocomplete for SupplierName
  onSupplierNameSelectionChanged(upitem) {
    this.supplierId = upitem.value
    this.InventoryForm.get('supplierId').setValue(upitem.value);
    this.InventoryForm.get('supplierName').setValue(upitem.text);
  }

  //autocomplete for SupplierName
  getSelectedSupplierName(supplierName) {
    this.ShopInventoryService.getSelectedSupplierName(supplierName, this.branchId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getPaymentStatusList() {
    this.ShopInventoryService.getSelectedPaymentStausList().subscribe(res => {
      this.paymentStatusList = res;
    });
  }

  getSelectedProductTypeList() {
    this.ShopInventoryService.getSelectedProductTypeList(this.branchId).subscribe(res => {
      this.productTypeList = res;
    });
  }

  getSelectedUnitList() {
    this.ShopInventoryService.getSelectedUnitList().subscribe(res => {
      this.unitList = res;
    });
  }
  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }



  //Generate Purchase Bill No
  generateBillNo() {
    this.ShopInventoryService.SpGetShopInventoryBillNo().subscribe(res => {
      console.log("🔍 Full API Response:", res);
      if (Array.isArray(res) && res.length > 0 && res[0].voucherNo) {
        this.processBillNo(res[0].voucherNo);
      } else {
        console.error("❌ Invalid API response format. Expected array with `Bill No`.");
        this.generateFirstBillNo();
      }
    });
  }
  processBillNo(lastUsed: string) {
    console.log("✅ Last Bill No:", lastUsed);

    let parts = lastUsed.split('-');
    if (parts.length < 3) {
      console.error("❌ Bill number format is invalid:", lastUsed);
      return;
    }

    let lastNumberPart = parts[2]; // '000001'
    let nextNumber = (parseInt(lastNumberPart) + 1).toString().padStart(6, '0');

    let today = new Date();
    let day = today.getDate().toString().padStart(2, '0');
    let monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    let month = monthNames[today.getMonth()];
    let year = today.getFullYear().toString().slice(-2); // '25'

    let datePart = `${day}${month}${year}`; // e.g., 29Jul25

    let newBillNo = `P-BILL-${nextNumber}-${datePart}`;
    console.log("✅ New Bill No:", newBillNo);

    this.InventoryForm.patchValue({ voucherNo: newBillNo });
  }
  generateFirstBillNo() {
    let firstNumber = '000001';

    let today = new Date();
    let day = today.getDate().toString().padStart(2, '0');
    let monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    let month = monthNames[today.getMonth()];
    let year = today.getFullYear().toString().slice(-2); // '25'

    let datePart = `${day}${month}${year}`; // e.g., 04Aug25

    let firstBillNo = `P-BILL-${firstNumber}-${datePart}`;
    console.log("✅ First Bill No:", firstBillNo);

    this.InventoryForm.patchValue({ voucherNo: firstBillNo });
  }


  get product() {
    return this.InventoryForm.get('shopInventoryDetail') as FormArray
  }
  onPurchaseQtyChange(i: number) {
    let salelessAmount = parseFloat(this.InventoryForm.get('lessAmount').value) || 0;
    let transportCost = parseFloat(this.InventoryForm.get('transportCost').value) || 0;
    let total = 0;
    for (let j = 0; j < this.product.length; j++) {
      let salePrice = parseFloat(this.product.at(j).get('unitPurchasePrice')?.value) || 0;
      let saleQty = parseFloat(this.product.at(j).get('totalUnitQty')?.value) || 0;
      total += (salePrice * saleQty);
    }
    total = total - salelessAmount + transportCost;
    this.InventoryForm.get('totalPurchasePrice').setValue(total);
    this.calculateSaleDueAmount();
  }

  addProductDetails(event: Event): void {
    event.preventDefault();
    console.log('Add product details')
    const control = <FormArray>this.InventoryForm.controls['shopInventoryDetail'];
    control.push(this.createProductDetailsForm());

  }
  removeProductDetail(index: number): void {
    const control = <FormArray>this.InventoryForm.controls['shopInventoryDetail'];
    const group = control.at(index) as FormGroup;
    const salePrice = parseFloat(group.get('unitPurchasePrice').value || 0); // Get salePrice, default to 0 if null
    const saleQty = parseFloat(group.get('totalUnitQty').value || 0);  // Get saleQty, default to 0 if null
    let transportCost = parseFloat(this.InventoryForm.get('transportCost').value);
    let salelessAmount = parseFloat(this.InventoryForm.get('lessAmount').value)
    const rowValue = (salePrice * saleQty) - salelessAmount + transportCost;  // Calculate row value

    // Subtract from totalSalePrice
    let currentTotal = parseFloat(this.InventoryForm.get('totalPurchasePrice').value || 0);
    this.InventoryForm.get('totalPurchasePrice').setValue(currentTotal - rowValue);
    if (index != 0) {
      this.product.removeAt(index);
    }
    this.calculateSaleDueAmount();
  }

  getRowTotal(i: number): void {

    const row = this.product.at(i);

    const qty = Number(row.get('totalUnitQty')?.value) || 0;
    const price = Number(row.get('unitPurchasePrice')?.value) || 0;

    // ===== Existing Code =====
    row.get('totalUnitPurchasePrice')
      ?.setValue((qty * price).toFixed(2), { emitEvent: false });

    // ===== New Code =====
    const transportCost = Number(row.get('transportCost')?.value) || 0;

    let costingPrice = price;

    if (qty > 0) {
      costingPrice = price + (transportCost / qty);
    }

    row.get('costingPrice')
      ?.setValue(costingPrice.toFixed(2), { emitEvent: false });

    // ===== Existing Code =====
    this.calculateGrandTotal();

  }
  getAllRowsTotal(): number {
    return this.product.controls.reduce((sum, row) => {
      const fg = row as FormGroup;
      const qty = parseFloat(fg.get('totalUnitQty')?.value) || 0;
      const price = parseFloat(fg.get('unitPurchasePrice')?.value) || 0;
      return sum + (qty * price);
    }, 0);
  }

  calculateGrandTotal(): void {
    let subTotal = 0;
    let totalTransportCost = 0;
    this.product.controls.forEach((row: AbstractControl) => {
      const qty = Number(row.get('totalUnitQty')?.value) || 0;
      const price = Number(row.get('unitPurchasePrice')?.value) || 0;
      const transport = Number(row.get('transportCost')?.value) || 0;
      totalTransportCost += transport;
      const rowTotal = qty * price;
      row.get('totalUnitPurchasePrice')?.setValue(rowTotal.toFixed(2), {
        emitEvent: false
      });

      subTotal += rowTotal;

    });

    const discount = Number(this.InventoryForm.get('lessAmount')?.value) || 0;
    const transport = totalTransportCost;
    const grandTotal = subTotal - discount + transport;
    this.InventoryForm.patchValue({
      transportCost: totalTransportCost.toFixed(2),
      totalPurchasePrice: grandTotal.toFixed(2)
    }, { emitEvent: false });

    this.calculateSaleDueAmount();

  }
  calculateSaleDueAmount(): void {
    const total = Number(this.InventoryForm.get('totalPurchasePrice')?.value) || 0;
    const paid = Number(this.InventoryForm.get('paidAmount')?.value) || 0;
    const due = total - paid;
    this.InventoryForm.patchValue({
      dueAmount: due.toFixed(2)
    }, { emitEvent: false });

  }

  calculateSalesLessAmount(): void {
    this.calculateGrandTotal();
  }
  calculateTolyRentAmount(): void {
    this.calculateGrandTotal();
  }
  onSalePaidAmountChange(value: string): void {

    const amount = Number(value) || 0;

    this.showWeightingScaleNo = amount > 0;

    this.calculateSaleDueAmount();

  }


  getWarehouseList() {
    this.ShopInventoryService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }




  formatDate(date: Date): string {
    date = new Date(date);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  onSubmit() {
    const id = this.InventoryForm.get('shopInventoryId').value;
    this.InventoryForm.get('purchaseDate').setValue(this.formatDate(this.InventoryForm.get('purchaseDate').value));

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ShopInventoryService.update(+id, this.InventoryForm.value).subscribe(response => {
            this.router.navigateByUrl('/fishproduct-stock/shopinventory-list');
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
      //this.InventoryForm.get('availableQty').setValue(this.InventoryForm.get('totalUnitQty').value);
      this.ShopInventoryService.submit(this.InventoryForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/fishproduct-stock/shopinventory-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}