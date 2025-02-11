import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { MAIN_MENU } from 'src/Utilities/common/app-data';
import { SIDE_MENU } from 'src/app/models/common/main';
import { Site } from 'src/app/models/dto/site';
import { User } from 'src/app/models/dto/user';
import { AccountService } from 'src/app/services/account.service';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { MessagingService } from 'src/app/services/common/messaging.service';
import { SubNavigationService } from 'src/app/services/main/sub-navigation.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, AfterViewInit {
  menu: SIDE_MENU[] = MAIN_MENU
  routes: string[] = []
  subRoutes: string[] = []
  currentPath!: string
  subPath!: string
  currentUser!: User
  sites!: Site[]

  siteId = new FormControl()

  constructor(private router: Router,
    private commonService: CommonService,
    private locationService: LocationService,
    private accountService: AccountService,
    private subNavService: SubNavigationService,
    private messagingService: MessagingService) {

  }

  ngOnInit() {
    // Routes for the Breadcrumbs
    this.router.events.subscribe(_ => {
      this.routes = this.router.url.split('/')
        .filter(r => r)
        .splice(1, this.router.url.length)
        .map(r => this.commonService.capitalizeFirstLetter(r))
      this.currentPath = this.routes[this.routes.length - 1]
      this.commonService.setTitle(this.currentPath)
      this.subRoutes = []
    })
  }

  async ngAfterViewInit() {
    this.messagingService.userSiteChanged().subscribe({
      next: async (response) => await this.updateUserProfile(),
      error: () => this.commonService.showErrorMessage()
    })

    await this.updateUserProfile()

    // Get Geolocation
    this.locationService.getCurrentLocation()

    this.subNavService.currentSubPath.subscribe(path => {
      if(!path)
        return
      this.subRoutes.push(path)
    })
  }

  async updateUserProfile() {
    // Fetch Profile
    let response = await this.accountService.getUserProfile(true)

    if(!response)
      this.commonService.showErrorMessage()

    this.currentUser = response.userResponse
    this.sites = response.sitesListResponse
  }

}
