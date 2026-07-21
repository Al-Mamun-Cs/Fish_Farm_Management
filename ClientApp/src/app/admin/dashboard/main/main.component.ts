import { Component, OnInit, ViewChild } from "@angular/core";
import { DashboardService } from "../service/Dashboard.service";
import { AuthService } from 'src/app/core/service/auth.service';
import { ChartComponent, ApexAxisChartSeries, ApexChart, ApexXAxis, ApexDataLabels, ApexTooltip, ApexYAxis, ApexPlotOptions, ApexStroke, ApexLegend, ApexFill, } from "ng-apexcharts";
import { MasterData } from "src/assets/data/master-data";
import { DatePipe } from "@angular/common";
import { Role } from 'src/app/core/models/role';

export type areaChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  dataLabels: ApexDataLabels;
  legend: ApexLegend;
  colors: string[];
};

export type barChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  colors: string[];
};

@Component({
  selector: "app-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
})
export class MainComponent implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public areaChartOptions: Partial<areaChartOptions>;
  public barChartOptions: Partial<barChartOptions>;
  masterData = MasterData;
  userRole = Role;
  //variables
  role: any;
  branchId: any;
  supplierId: any
  searchText = "";
  totalSupplierDueAmount: number = 0;
  dailyTotalSalesAmount: number = 0;
  totalFisheriesProductTypeCount: any;
  totalFisheriesPondCount: any;
  totalShopProductCount: any;
  totalDailyCost:number = 0;
  totalCashCapital:any;
  totalCashInHand:number = 0;
  totalCashInHandDetail:any;

  helpLineList: any;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: this.masterData.paging.pageSize,
    length: 1,
  };
  groupArrays: { libraryName: string; datas: any }[];


  constructor(
    private datepipe: DatePipe, private dashboardService: DashboardService, private authService: AuthService) {

  }

  ngOnInit() {
    this.role = this.authService.currentUserValue.role.trim();
    this.branchId = this.authService.currentUserValue.branchId.trim();
    this.supplierId = this.authService.currentUserValue.supplierId.toString().trim();
    console.log(this.role, this.branchId, this.supplierId, "user data")

    this.getTotalSupplierDueAmount();
    this.getDailyTotalSalesAmount();
    this.getTotalFisheriesProductTypeList();
    this.getTotalFisheriesPondList();
    this.getTotalShopProductList();
    this.getDailyCostTotal();
    this.getTotalCashCapital();
    this.getTotalCashInHand();
  }

  getTotalSupplierDueAmount() {
    this.dashboardService.getTotalSupplierDueAmount(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.totalSupplierDueAmount = response[0].totalDueAmount;
      }
      console.log(this.totalSupplierDueAmount);
    });
  }

  getDailyTotalSalesAmount() {
    this.dashboardService.getDailyTotalSalesAmount(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.dailyTotalSalesAmount = response[0].grandTotalSalePrice;
      }
      console.log(this.dailyTotalSalesAmount,"dailyTotalSalesAmount");
    });
  }

  getTotalFisheriesProductTypeList() {
    this.dashboardService.getTotalFisheriesProductTypeList(this.branchId).subscribe((response) => {
      this.totalFisheriesProductTypeCount = response;
      console.log(this.totalFisheriesProductTypeCount, "1 ProductType")
    });
  }

  getTotalFisheriesPondList() {
    this.dashboardService.getTotalFisheriesPondList(this.branchId).subscribe((response) => {
      this.totalFisheriesPondCount = response;
      console.log(this.totalFisheriesPondCount, "2 Pond")
    });
  }

  get grandTotalFisheriesPondCount(): number {
    return this.totalFisheriesPondCount?.reduce((sum, item) => {
      return sum + Number(item.totalCost || 0);
    }, 0) || 0;
  }

  getTotalShopProductList() {
    this.dashboardService.getTotalShopProductList(this.branchId).subscribe((response) => {
      this.totalShopProductCount = response;
      console.log(this.totalShopProductCount, "1 ProductType")
    });
  }

  getDailyCostTotal() {
    this.dashboardService.getDailyCostTotal(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.totalDailyCost = response[0].totalCostAmount;
      }
      console.log(this.totalDailyCost);
    });
  }

  getTotalCashCapital() {
    this.dashboardService.getTotalCashCapital(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.totalCashCapital = response[0].totalCapital;
      }
      console.log(this.totalCashCapital);
    });
  }

   getTotalCashInHand() {
    this.dashboardService.getTotalCashInHand(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.totalCashInHand = response[0].totalCashInHand;
      }
      console.log(this.totalCashInHand);
    });
  }

  getCashInHandDetail() {
    this.dashboardService.getCashInHandDetail(this.branchId).subscribe((response: any) => {
      if (response && response.length > 0) {
        this.totalCashInHandDetail = response[0].totalCapital;
      }
      console.log(this.totalCashInHandDetail);
    });
  }



}
