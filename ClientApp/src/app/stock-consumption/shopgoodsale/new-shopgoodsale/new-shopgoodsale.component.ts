import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopGoodSaleService } from '../../service/ShopGoodSale.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { AuthService } from 'src/app/core/service/auth.service';
import { DatePipe } from '@angular/common';
import { SupplierService } from '../../../basic-setup/service/Supplier.service';
import { HostListener } from '@angular/core';

@Component({
  selector: 'app-new-shopgoodsale',
  templateUrl: './new-shopgoodsale.component.html',
  styleUrls: ['./new-shopgoodsale.component.sass']
})
export class NewShopGoodSaleComponent implements OnInit {
  buttonText: string;
  pageTitle: string;
  destination: string;
  InventoryForm: FormGroup;
  validationErrors: string[] = [];
  unitList: SelectedModel[];
  productTypeList: SelectedModel[];
  productNameList: SelectedModel[];
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


  constructor(private snackBar: MatSnackBar, private authService: AuthService, private datePipe: DatePipe, private SupplierService: SupplierService, private confirmService: ConfirmService, private ShopGoodSaleService: ShopGoodSaleService, private fb: FormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    console.log(this.role, this.branchId)

    const id = this.route.snapshot.paramMap.get('shopGoodSaleId');

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
      shopGoodSaleId: [0],
      warehouseId: [],
      supplierId: [],
      supplierName: [""],
      paymentStatusId: [],
      voucherNo: [''],
      saleDate: [today],
      totalSalePrice: [],
      saleLessAmount: [0],
      grandTotalSalePrice: [],
      customerPaidAmount: [],
      customerDueAmount: [0],
      remarks: [],
      approveStatus: [0],
      isActive: [true],
      shopGoodSaleDetail: this.fb.array([
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
      shopGoodSaleDetailId: [0],
      warehouseId: [0],
      shopGoodSaleId: [],
      fisheriesProductTypeId: [],
      fisheriesUnitId: [],
      shopInventoryDetailId: [],
      saleQty: [],
      availableQty: [],
      salePrice: [],
      rowTotalSalePrice: [],
      costingPrice: [],
      unitPurchasePrice: [0],
      profit: [0],
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
    this.ShopGoodSaleService.getSelectedSupplierName(supplierName, this.branchId).subscribe(response => {
      this.options = response;
      this.filteredOptions = response;
    })
  }
  getWarehouseList() {
    this.ShopGoodSaleService.getSelectedWarehousesList().subscribe(res => {
      this.warehouseList = res;
    });
  }

  getPaymentStatusList() {
    this.ShopGoodSaleService.getSelectedPaymentStausList().subscribe(res => {
      this.paymentStatusList = res;
    });
  }

  getSelectedProductTypeList() {
    this.ShopGoodSaleService.getSelectedProductTypeList(this.branchId).subscribe(res => {
      this.productTypeList = res;
    });
  }
  getSelectedUnitList() {
    this.ShopGoodSaleService.getSelectedUnitList().subscribe(res => {
      this.unitList = res;
      console.log(res, "unitList");
    });
  }

  onProductTypeChange(index: number) {
    const productTypeId = this.product.at(index).get('fisheriesProductTypeId').value;
    this.ShopGoodSaleService
      .GetShopInventoryProductName(this.branchId, productTypeId)
      .subscribe(res => {
        this.productNameList = res;
        console.log(res, "productNameList");

      });

  }

  getShopDetailData(index: number) {
    const shopInventoryDetailId = this.product.at(index).get('shopInventoryDetailId')?.value;
    console.log(shopInventoryDetailId, "shop Detail Data1");

    this.ShopGoodSaleService.findShopDetail(shopInventoryDetailId).subscribe(res => {
      console.log(res, "shop Detail Data");
      this.product.at(index).patchValue({
        costingPrice: res.costingPrice,
        availableQty: res.availableQty
      });

    });

  }
  checkSaleQty(index: number): void {

    const row = this.product.at(index);

    const availableQty = Number(row.get('availableQty')?.value) || 0;
    const saleQty = Number(row.get('saleQty')?.value) || 0;

    if (saleQty > availableQty) {

      this.snackBar.open(
        `Available Qty মাত্র ${availableQty}. এর বেশি বিক্রি করা যাবে না।`,
        '',
        {
          duration: 3000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
          panelClass: 'snackbar-danger'
        }
      );

      // saleQty reset করে দাও
      row.get('saleQty')?.setValue(availableQty);

      return;
    }

    // Qty ঠিক থাকলে calculation হবে
    this.getRowTotal(index);
  }



  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }



  //Generate Purchase Bill No
  generateBillNo() {
    this.ShopGoodSaleService.SpGetShopGoodSaleBillNo().subscribe(res => {
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
    return this.InventoryForm.get('shopGoodSaleDetail') as FormArray
  }
  onPurchaseQtyChange(i: number) {
    let salelessAmount = parseFloat(this.InventoryForm.get('saleLessAmount').value) || 0;
    let total = 0;
    for (let j = 0; j < this.product.length; j++) {
      let salePrice = parseFloat(this.product.at(j).get('salePrice')?.value) || 0;
      let saleQty = parseFloat(this.product.at(j).get('saleQty')?.value) || 0;
      total += (salePrice * saleQty);
    }
    total = total - salelessAmount;
    this.InventoryForm.get('totalSalePrice').setValue(total);
    this.calculateSaleDueAmount();
  }

  addProductDetails(event: Event): void {
    event.preventDefault();
    console.log('Add product details')
    const control = <FormArray>this.InventoryForm.controls['shopGoodSaleDetail'];
    control.push(this.createProductDetailsForm());

  }
  removeProductDetail(index: number): void {
    const control = <FormArray>this.InventoryForm.controls['shopGoodSaleDetail'];
    const group = control.at(index) as FormGroup;
    const salePrice = parseFloat(group.get('salePrice').value || 0);
    const saleQty = parseFloat(group.get('saleQty').value || 0);
    let salelessAmount = parseFloat(this.InventoryForm.get('saleLessAmount').value)
    const rowValue = (salePrice * saleQty) - salelessAmount;

    // Subtract from totalSalePrice
    let currentTotal = parseFloat(this.InventoryForm.get('totalSalePrice').value || 0);
    this.InventoryForm.get('totalSalePrice').setValue(currentTotal - rowValue);
    if (index != 0) {
      this.product.removeAt(index);
    }
    this.calculateSaleDueAmount();
  }

  getRowTotal(i: number): void {

    const row = this.product.at(i);

    const qty = Number(row.get('saleQty')?.value) || 0;
    const price = Number(row.get('salePrice')?.value) || 0;
    const costingPrice = Number(row.get('costingPrice')?.value) || 0;

    // ===== Existing Code =====
    row.get('rowTotalSalePrice')
      ?.setValue((qty * price).toFixed(2), { emitEvent: false });

    // Profit
    const profit = (price - costingPrice) * qty;
    row.get('profit')?.setValue(profit.toFixed(2), { emitEvent: false });
    // ===== Existing Code =====
    this.calculateGrandTotal();

  }
  getAllRowsTotal(): number {
    return this.product.controls.reduce((sum, row) => {
      const fg = row as FormGroup;
      const qty = parseFloat(fg.get('saleQty')?.value) || 0;
      const price = parseFloat(fg.get('salePrice')?.value) || 0;
      return sum + (qty * price);
    }, 0);
  }

  calculateGrandTotal() {

    let subTotal = 0;

    this.product.controls.forEach(row => {

      const qty = Number(row.get('saleQty').value) || 0;

      const price = Number(row.get('salePrice').value) || 0;

      const rowTotal = qty * price;

      row.get('rowTotalSalePrice')
        .setValue(rowTotal.toFixed(2), { emitEvent: false });

      subTotal += rowTotal;

    });

    const discount =
      Number(this.InventoryForm.get('saleLessAmount').value) || 0;

    const grandTotal = subTotal - discount;

    this.InventoryForm.patchValue({

      totalSalePrice: subTotal.toFixed(2),

      grandTotalSalePrice: grandTotal.toFixed(2)

    }, { emitEvent: false });

    this.calculateSaleDueAmount();

  }
  calculateSaleDueAmount() {

    const grandTotal =
      Number(this.InventoryForm.get('grandTotalSalePrice').value) || 0;

    const paid =
      Number(this.InventoryForm.get('customerPaidAmount').value) || 0;

    const due = grandTotal - paid;

    this.InventoryForm.patchValue({

      customerDueAmount: due.toFixed(2)

    }, { emitEvent: false });

  }

  calculateSalesLessAmount(): void {
    this.calculateGrandTotal();
  }

  onSalePaidAmountChange(value: string): void {

    const amount = Number(value) || 0;

    this.showWeightingScaleNo = amount > 0;

    this.calculateSaleDueAmount();

  }








  formatDate(date: Date): string {
    date = new Date(date);
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  onSubmit() {
    const id = this.InventoryForm.get('shopGoodSaleId').value;
    this.InventoryForm.get('saleDate').setValue(this.formatDate(this.InventoryForm.get('saleDate').value));

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This Item?').subscribe(result => {
        console.log(result);
        if (result) {
          this.ShopGoodSaleService.update(+id, this.InventoryForm.value).subscribe(response => {
            this.router.navigateByUrl('/stock-consumption/shopgoodsale-list');
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
      this.ShopGoodSaleService.submit(this.InventoryForm.value).subscribe(response => {
        this.snackBar.open('Information Saved Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
        this.router.navigateByUrl('/stock-consumption/shopgoodsale-list');
      }, error => {
        this.validationErrors = error;
      })
    }

  }

}