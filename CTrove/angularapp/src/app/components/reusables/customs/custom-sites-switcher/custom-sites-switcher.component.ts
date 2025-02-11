import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormControl } from '@angular/forms';
import { SITE_SWITCHED } from 'src/Utilities/common/app-strings';
import { Site } from 'src/app/models/dto/site';
import { AccountService } from 'src/app/services/account.service';
import { CommonService } from 'src/app/services/common/common.service';

@Component({
  selector: 'app-custom-sites-switcher',
  templateUrl: './custom-sites-switcher.component.html',
  styleUrls: ['./custom-sites-switcher.component.css']
})
export class CustomSitesSwitcherComponent implements OnInit {
  @Input() sites!: Site[]
  siteId = new FormControl()

  constructor(private accountService: AccountService,
    private commonService: CommonService) { }

  ngOnChanges(changes: SimpleChanges) {
    if((changes['sites']).firstChange || !(changes['sites']).firstChange) {
      if(this.sites && this.sites.length > 0) {
        let savedSite = this.accountService.getUserSite()
         this.siteId.setValue(savedSite ? savedSite.id : this.sites[0].id)
      }
      else
        this.siteId.setValue(null)
    }
  }

  ngOnInit() {
    this.siteId.valueChanges.subscribe(siteId => {
      let site = this.sites.find(d => d.id === siteId)
      if(!site)
        return
      this.accountService.setUserSite(site)
      this.commonService.showMessage(SITE_SWITCHED)
    })
  }
}