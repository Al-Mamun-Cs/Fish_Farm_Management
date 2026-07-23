import { ConfigService } from '../../config/config.service';
import { DOCUMENT } from '@angular/common';
import { Component, Inject, ElementRef, OnInit, Renderer2, AfterViewInit, } from '@angular/core';
import { AuthService } from 'src/app/core/service/auth.service';
import { Router } from '@angular/router';
import { RightSidebarService } from 'src/app/core/service/rightsidebar.service';
import { Role } from 'src/app/core/models/role';
import { LanguageService } from 'src/app/core/service/language.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { BaseSchoolNameService } from 'src/app/security/service/BaseSchoolName.service';
import { HostListener } from '@angular/core';

const document: any = window.document;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.sass'],
})
export class HeaderComponent
  extends UnsubscribeOnDestroyAdapter
  implements OnInit, AfterViewInit {
  public config: any = {};
  memberInfoId: any;
  userName: any;
  wHouse: any;
  libraryName: any;
  schoolLogo: any;
  loggedUserRole: any;
  userRole = Role;
  userEmail: any;
  userImg: string;
  homePage: string;
  isNavbarCollapsed = true;
  showMobileMenu = false;
  showPurchaseMenu = false;
  showStockConsumptionMenu = false;
  showFinancialTransactionsMenu = false;
  showSetupMenu = false;
  showDashboardMenu = false;
  selectedMenuData: string = '';

  flagvalue;
  branchId: any;
  countryName;
  langStoreValue: string;
  defaultFlag: string;
  isOpenSidebar: boolean;
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private renderer: Renderer2,
    public elementRef: ElementRef,
    private rightSidebarService: RightSidebarService,
    private baseSchoolNameService: BaseSchoolNameService,
    private configService: ConfigService,
    private authService: AuthService,
    private router: Router,
    public languageService: LanguageService
  ) {
    super();
  }
  listLang = [
    { text: 'English', flag: 'assets/images/flags/us.jpg', lang: 'en' },
    { text: 'Spanish', flag: 'assets/images/flags/spain.jpg', lang: 'es' },
    { text: 'German', flag: 'assets/images/flags/germany.jpg', lang: 'de' },
  ];
  notifications: any[] = [
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'Sarah Smith',
      time: '14 mins ago',
      message: 'Please check your mail',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'Airi Satou',
      time: '22 mins ago',
      message: 'Work Completed !!!',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'John Doe',
      time: '3 hours ago',
      message: 'kindly help me for code.',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'Ashton Cox',
      time: '5 hours ago',
      message: 'Lets break for lunch...',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'Sarah Smith',
      time: '14 mins ago',
      message: 'Please check your mail',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'Airi Satou',
      time: '22 mins ago',
      message: 'Work Completed !!!',
    },
    {
      userImg: 'assets/images/user/user1.jpg',
      userName: 'John Doe',
      time: '3 hours ago',
      message: 'kindly help me for code.',
    },
  ];
  ngOnInit() {
    this.config = this.configService.configData;

    this.loggedUserRole = this.authService.currentUserValue.role;
    this.userName = this.authService.currentUserValue.username;
    this.userEmail = this.authService.currentUserValue.email;
    this.userImg = this.authService.currentUserValue.img;
    this.branchId = this.authService.currentUserValue.branchId;
    console.log("header user" + this.userName)
    this.homePage = 'admin/dashboard/main';


    // if(this.loggedUserRole != this.userRole.SuperAdmin && this.loggedUserRole != this.userRole.Member){
    //   this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
    //     console.log(res)
    //     this.libraryName = res.schoolName;
    //     this.schoolLogo =res.schoolLogo;
    //     console.log(this.libraryName+ "_"+ this.schoolLogo)
    //     console.log("Find School Name")
    //   });
    // }


    this.langStoreValue = localStorage.getItem('lang');
    const val = this.listLang.filter((x) => x.lang === this.langStoreValue);
    this.countryName = val.map((element) => element.text);
    if (val.length === 0) {
      if (this.flagvalue === undefined) {
        this.defaultFlag = 'assets/images/flags/us.jpg';
      }
    } else {
      this.flagvalue = val.map((element) => element.flag);
    }
    const currentUrl = this.router.url;
    this.showDashboardMenu = currentUrl.includes('dashboard');
    this.showSetupMenu = currentUrl.includes('paymentstatus-list') || currentUrl.includes('supplier-list') || currentUrl.includes('fisheriesunit-list') 
    || currentUrl.includes('fisheriesproducttype-list') || currentUrl.includes('pond-list') || currentUrl.includes('dailycostvaucherreason-list');
    this.showPurchaseMenu = currentUrl.includes('fisheriesinventory-list') || currentUrl.includes('shopinventory-list');
    this.showStockConsumptionMenu = currentUrl.includes('fisheriesinventoryout-list') || currentUrl.includes('shopgoodsale-list');
    this.showFinancialTransactionsMenu = currentUrl.includes('dailymiscellaneouscost-list') || currentUrl.includes('shophandcashwithdrow-list') 
    || currentUrl.includes('investment-list');
  }
  ngAfterViewInit() {
    // set theme on startup
    if (localStorage.getItem('theme')) {
      this.renderer.removeClass(this.document.body, this.config.layout.variant);
      this.renderer.addClass(this.document.body, localStorage.getItem('theme'));
    } else {
      this.renderer.addClass(this.document.body, this.config.layout.variant);
    }

    if (localStorage.getItem('menuOption')) {
      this.renderer.addClass(
        this.document.body,
        localStorage.getItem('menuOption')
      );
    } else {
      this.renderer.addClass(
        this.document.body,
        'menu_' + this.config.layout.sidebar.backgroundColor
      );
    }

    if (localStorage.getItem('choose_logoheader')) {
      this.renderer.addClass(
        this.document.body,
        localStorage.getItem('choose_logoheader')
      );
    } else {
      this.renderer.addClass(
        this.document.body,
        'logo-' + this.config.layout.logo_bg_color
      );
    }

    if (localStorage.getItem('sidebar_status')) {
      if (localStorage.getItem('sidebar_status') === 'close') {
        this.renderer.addClass(this.document.body, 'side-closed');
        this.renderer.addClass(this.document.body, 'submenu-closed');
      } else {
        this.renderer.removeClass(this.document.body, 'side-closed');
        this.renderer.removeClass(this.document.body, 'submenu-closed');
      }
    } else {
      if (this.config.layout.sidebar.collapsed === true) {
        this.renderer.addClass(this.document.body, 'side-closed');
        this.renderer.addClass(this.document.body, 'submenu-closed');
      }
    }
  }
  callFullscreen() {
    if (
      !document.fullscreenElement &&
      !document.mozFullScreenElement &&
      !document.webkitFullscreenElement &&
      !document.msFullscreenElement
    ) {
      if (document.documentElement.requestFullscreen) {
        document.documentElement.requestFullscreen();
      } else if (document.documentElement.msRequestFullscreen) {
        document.documentElement.msRequestFullscreen();
      } else if (document.documentElement.mozRequestFullScreen) {
        document.documentElement.mozRequestFullScreen();
      } else if (document.documentElement.webkitRequestFullscreen) {
        document.documentElement.webkitRequestFullscreen();
      }
    } else {
      if (document.exitFullscreen) {
        document.exitFullscreen();
      } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
      } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
      } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
      }
    }
  }
  setLanguage(text: string, lang: string, flag: string) {
    this.countryName = text;
    this.flagvalue = flag;
    this.langStoreValue = lang;
    this.languageService.setLanguage(lang);
  }
  mobileMenuSidebarOpen(event: any, className: string) {
    const hasClass = event.target.classList.contains(className);
    if (hasClass) {
      this.renderer.removeClass(this.document.body, className);
    } else {
      this.renderer.addClass(this.document.body, className);
    }
  }
  toggleMobileMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showMobileMenu = !this.showMobileMenu;
    if (this.showMobileMenu) {
      this.showDashboardMenu = false;
      this.showSetupMenu = false;
      this.showPurchaseMenu = false;
      this.showStockConsumptionMenu = false;
      this.showFinancialTransactionsMenu = false;
    }
  }

  closeMobileMenu() {
    this.showMobileMenu = false;
  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    this.showMobileMenu = false;
  }
  callSidemenuCollapse() {
    const hasClass = this.document.body.classList.contains('side-closed');
    if (hasClass) {
      this.renderer.removeClass(this.document.body, 'side-closed');
      this.renderer.removeClass(this.document.body, 'submenu-closed');
    } else {
      this.renderer.addClass(this.document.body, 'side-closed');
      this.renderer.addClass(this.document.body, 'submenu-closed');
    }
  }
  public toggleRightSidebar(): void {
    this.subs.sink = this.rightSidebarService.sidebarState.subscribe(
      (isRunning) => {
        this.isOpenSidebar = isRunning;
      }
    );

    this.rightSidebarService.setRightSidebar(
      (this.isOpenSidebar = !this.isOpenSidebar)
    );
  }
  logout() {
    this.subs.sink = this.authService.logout().subscribe((res) => {
      if (!res.success) {
        this.router.navigate(['/authentication/signin']);
      }
    });
  }

  toggleDashboardMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showDashboardMenu = !this.showDashboardMenu;
  }

  toggleSetupMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showSetupMenu = !this.showSetupMenu;
  }
  togglePurchaseMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showPurchaseMenu = !this.showPurchaseMenu;
  }

  toggleStockConsumptionMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showStockConsumptionMenu = !this.showStockConsumptionMenu;
  }

  toggleFinancialTransactionsMenu(event: MouseEvent) {
    event.stopPropagation();
    this.showFinancialTransactionsMenu = !this.showFinancialTransactionsMenu;
  }

  onSubMenuClick(menuType: string) {
    console.log("Sub menu clicked:", menuType);
    this.selectedMenuData = `Showing data for ${menuType}`;

    // ক্লিক অনুযায়ী নির্দিষ্ট মেনু ওপেন থাকবে, বাকিগুলো বন্ধ হবে
    this.showDashboardMenu = (menuType === 'dashboard');
    this.showSetupMenu = (menuType === 'setup');
    this.showPurchaseMenu = (menuType === 'purchase');
    this.showStockConsumptionMenu = (menuType === 'stockCom');
    this.showFinancialTransactionsMenu = (menuType === 'financial');

    // মোবাইল মেনু বন্ধ করা
    this.showMobileMenu = false;
  }
  isUrlActive(url: string): boolean {
    return this.router.url.includes(url);
  }
}
