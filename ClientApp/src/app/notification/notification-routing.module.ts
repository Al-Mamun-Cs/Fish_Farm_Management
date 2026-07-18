import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';

import { BulletinListComponent } from './bulletin/bulletin-list/bulletin-list.component';
import { NewBulletinComponent } from './bulletin/new-bulletin/new-bulletin.component';
import { NoticeInfoListComponent } from './noticeinfo/noticeinfo-list/noticeinfo-list.component';
import { NewNoticeInfoComponent } from './noticeinfo/new-noticeinfo/new-noticeinfo.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },
  
  {
    path: 'bulletin-list',
    component: BulletinListComponent,
  },
  { path: 'update-bulletin/:bulletinId', 
  component:  NewBulletinComponent,
  },
  {
    path: 'add-bulletin',
    component: NewBulletinComponent,
  },

  {
    path: 'noticeinfo-list',
    component: NoticeInfoListComponent,
  },
  { path: 'update-noticeinfo/:noticeInfoId', 
  component:  NewNoticeInfoComponent,
  },
  {
    path: 'add-noticeinfo',
    component: NewNoticeInfoComponent,
  },

  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class NotificationRoutingModule { }
