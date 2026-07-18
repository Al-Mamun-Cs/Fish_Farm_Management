import { Component, OnInit, ViewChild } from "@angular/core";
import { DashboardService } from "../service/Dashboard.service";
import { AuthService } from 'src/app/core/service/auth.service';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexTooltip,
  ApexYAxis,
  ApexPlotOptions,
  ApexStroke,
  ApexLegend,
  ApexFill,
} from "ng-apexcharts";
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

  totalFisheriesProductTypeCount: any;
  totalFisheriesPondCount:any;

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

    this.getTotalFisheriesProductTypeList();
    this.getTotalFisheriesPondList();
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




}
