import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/core/service/auth.service';
import { MasterData } from 'src/assets/data/master-data';
import { DashboardService } from "../service/Dashboard.service";
import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexYAxis, ApexDataLabels, ApexStroke, ApexMarkers, ApexGrid, ApexTooltip, ApexLegend} from 'ng-apexcharts';


export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  dataLabels: ApexDataLabels;
  stroke: ApexStroke;
  markers: ApexMarkers;
  grid: ApexGrid;
  tooltip: ApexTooltip;
  legend: ApexLegend;
};


@Component({
  selector: 'app-dailyfishfeedconsumption-list',
  templateUrl: './dailyfishfeedconsumption-list.component.html',
  styleUrls: ['./dailyfishfeedconsumption-list.component.sass']
})
export class DailyFishFeedConsumptionListComponent implements OnInit {

  photoBaseUrl = '';
  masterData = MasterData;
  isLoading = false;
  showHideDiv: any;
  pageTitle: any;
  role: any;
  branchId: any;
  supplierId: any;
  projectScheduleId: any;
  dailyFishFeedConsumption: any[] = [];
  searchText = "";
  permission: any;
  grandTotalCash = 0;
  grandTotalBank = 0;
  grandTotalCapital = 0;
  grandTotalReturnableAmount = 0;
  paging = {
    pageIndex: this.masterData.paging.pageIndex,
    pageSize: 100,
    length: 1
  };


  public chartOptions: Partial<ChartOptions>;

  constructor(
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private DashboardService: DashboardService,
    private _location: Location,
    private router: Router,
    private route: ActivatedRoute
  ) {

    this.chartOptions = {

      series: [{ name: 'Feed Consumption', data: []}],

      chart: { type: 'line', height: 430,
        toolbar: {
          show: true
        },
        zoom: {
          enabled: true
        },
        animations: {
          enabled: true
        }
      },

      stroke: {
        curve: 'straight',
        width: 3
      },

      markers: {
        size: 5,
        strokeWidth: 2,
        hover: {
          size: 7
        }
      },

      dataLabels: {
        enabled: false
      },

      xaxis: {
        categories: [],
        title: {
          text: 'Days'
        },

        labels: {
          rotate: -45,
          hideOverlappingLabels: true,
          trim: false
        }
      },

      yaxis: {
        min: 0,

        title: {
          text: 'Feed Consumption (kg)'
        },

        labels: {
          formatter: function (value) {
            return value.toFixed(0) + ' kg';
          }
        }
      },

      grid: {
        show: true,
        borderColor: '#e5e5e5',
        strokeDashArray: 4,
        xaxis: {
          lines: {
            show: false
          }
        },
        yaxis: {
          lines: {
            show: true
          }
        }
      },

      tooltip: {
        shared: false,
        intersect: true,

        custom: ({ series, seriesIndex, dataPointIndex }) => {

          const data =
            this.dailyFishFeedConsumption[dataPointIndex];

          if (!data) {
            return '';
          }

          const feedQty =
            Number(data.totalUseQty || 0);

          const totalPrice =
            Number(data.totalPrice || 0);

          const date =
            data.consumptionDate
              ? new Date(data.consumptionDate)
              : null;

          let formattedDate = '';

          if (date) {

            const day =
              String(date.getDate()).padStart(2, '0');

            const monthNames = [
              'Jan',
              'Feb',
              'Mar',
              'Apr',
              'May',
              'Jun',
              'Jul',
              'Aug',
              'Sep',
              'Oct',
              'Nov',
              'Dec'
            ];

            const month =
              monthNames[date.getMonth()];

            const year =
              date.getFullYear();

            formattedDate =
              `${day}-${month}-${year}`;
          }


          return `
            <div
              style="
                padding: 12px 15px;
                min-width: 190px;
                background: #ffffff;
                border-radius: 6px;
              "
            >

              <div
                style="
                  font-weight: 600;
                  font-size: 15px;
                  margin-bottom: 8px;
                  color: #11205F;
                "
              >
                Day ${data.dayNumber}
              </div>

              <div
                style="
                  font-size: 13px;
                  margin-bottom: 5px;
                "
              >
                Date:
                <strong>${formattedDate}</strong>
              </div>

              <div
                style="
                  font-size: 13px;
                  margin-bottom: 5px;
                "
              >
                Feed Consumption:
                <strong>${feedQty.toFixed(2)} kg</strong>
              </div>

              <div
                style="
                  font-size: 13px;
                "
              >
                Total Price:
                <strong>${totalPrice.toFixed(2)}</strong>
              </div>

            </div>
          `;
        }
      },


      // -----------------------------------------------------
      // LEGEND
      // -----------------------------------------------------

      legend: {
        show: true,
        position: 'top',
        horizontalAlign: 'right'
      }

    };
  }


  // =========================================================
  // NG ON INIT
  // =========================================================

  ngOnInit() {

    this.role =
      this.authService.currentUserValue.role.trim();

    this.branchId =
      this.authService.currentUserValue.branchId.trim();

    this.supplierId =
      this.authService.currentUserValue.supplierId
        .toString()
        .trim();


    console.log(
      this.role,
      this.branchId,
      this.supplierId,
      "employee Id"
    );


    // Get ProjectScheduleId from URL

    this.projectScheduleId =
      this.route.snapshot.paramMap
        .get('projectScheduleId');


    console.log(
      this.projectScheduleId,
      "Project Schedule Id"
    );


    // Get API Data

    this.SpGetDailyFishFeedConsumption();
  }


  // =========================================================
  // GET DAILY FISH FEED CONSUMPTION
  // =========================================================

  SpGetDailyFishFeedConsumption() {

    this.isLoading = true;


    this.DashboardService
      .SpGetDailyFishFeedConsumption(
        this.projectScheduleId
      )
      .subscribe(

        response => {

          this.dailyFishFeedConsumption =
            response || [];


          console.log(
            this.dailyFishFeedConsumption,
            "feed consumption data"
          );


          // Update Chart

          this.prepareChartData();


          this.isLoading = false;

        },

        error => {

          console.log(
            error,
            "feed consumption error"
          );

          this.dailyFishFeedConsumption = [];

          this.isLoading = false;
        }
      );
  }


  // =========================================================
  // PREPARE CHART DATA
  // =========================================================

  prepareChartData() {

    if (
      !this.dailyFishFeedConsumption ||
      this.dailyFishFeedConsumption.length === 0
    ) {

      this.chartOptions = {

        ...this.chartOptions,

        series: [
          {
            name: 'Feed Consumption',
            data: []
          }
        ],

        xaxis: {
          categories: []
        }

      };

      return;
    }


    // -------------------------------------------------------
    // X AXIS
    // -------------------------------------------------------

    const categories =
      this.dailyFishFeedConsumption.map(
        x => 'Day ' + x.dayNumber
      );


    // -------------------------------------------------------
    // Y AXIS DATA
    // -------------------------------------------------------

    const feedConsumption =
      this.dailyFishFeedConsumption.map(
        x => Number(x.totalUseQty) || 0
      );


    console.log(
      categories,
      "chart categories"
    );

    console.log(
      feedConsumption,
      "chart values"
    );


    // -------------------------------------------------------
    // SET CHART DATA
    // -------------------------------------------------------

    this.chartOptions = {

      ...this.chartOptions,

      series: [
        {
          name: 'Feed Consumption',
          data: feedConsumption
        }
      ],

      xaxis: {
        categories: categories,

        title: {
          text: 'Days',
          offsetY: -10
        },

        labels: {
          rotate: -45,
          hideOverlappingLabels: true,
          trim: false
        }
      }

    };
  }


  // =========================================================
  // BACK
  // =========================================================

  backClicked() {

    this._location.back();
  }


  // =========================================================
  // RELOAD CURRENT ROUTE
  // =========================================================

  reloadCurrentRoute() {

    const currentUrl =
      this.router.url;


    this.router
      .navigateByUrl(
        '/',
        {
          skipLocationChange: true
        }
      )
      .then(() => {

        this.router.navigate(
          [currentUrl]
        );

      });
  }
  getTotalUseQty(): number {
  return this.dailyFishFeedConsumption?.reduce(
    (total: number, item: any) => total + (Number(item.totalUseQty) || 0),
    0
  ) || 0;
}

getTotalPrice(): number {
  return this.dailyFishFeedConsumption?.reduce(
    (total: number, item: any) => total + (Number(item.totalPrice) || 0),
    0
  ) || 0;
}


  // =========================================================
  // PAGE CHANGE
  // =========================================================

  pageChanged(event: any) {

    this.paging.pageIndex =
      event.pageIndex;

    this.paging.pageSize =
      event.pageSize;

    this.paging.pageIndex =
      this.paging.pageIndex + 1;


    this.SpGetDailyFishFeedConsumption();
  }


  // =========================================================
  // SEARCH
  // =========================================================

  applyFilter(searchText: any) {

    this.searchText =
      searchText;

    this.SpGetDailyFishFeedConsumption();
  }


  // =========================================================
  // TOGGLE
  // =========================================================

  toggle() {

    this.showHideDiv =
      !this.showHideDiv;
  }

}