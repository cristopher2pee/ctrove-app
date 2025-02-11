import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../common/base-class';
import { USER_FIELDS, User } from 'src/app/models/dto/user';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageType } from 'src/Utilities/common/app-enums';
import { INVALID_INPUTS, ONBOARDING_SUCCESS } from 'src/Utilities/common/app-strings';
import { AccountService } from 'src/app/services/account.service';
import { RoutingService } from 'src/app/services/routing.service';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';

@Component({
  selector: 'app-onboarding',
  templateUrl: './onboarding.component.html',
  styleUrls: ['./onboarding.component.css']
})
export class OnboardingComponent extends BaseComponent implements OnInit {
  fields = USER_FIELDS
  timezones!: any[]

  form = new FormGroup({
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    middlename: new FormControl(),
    suffix: new FormControl(),
    prefix: new FormControl(),
    email: new FormControl('', [Validators.required]),
    mobile: new FormControl('', [Validators.required]),
    landline: new FormControl(),
    organization: new FormControl('', [Validators.required]),
    rolesId: new FormControl(),
    location: new FormControl(this.locationService.getLocalTimezone())
  })

  constructor(private commonService: CommonService,
    private accountService: AccountService,
    private routingService: RoutingService,
    private locationService: LocationService) {
    super();
  }

  ngOnInit() {
    // Get timezones
    this.timezones = this.commonService.getTimezones()

    // Get location
    this.locationService.getCurrentLocation()

    // Get Profile
    this.accountService.getProfile().subscribe({
      next: (response) => {
        this.form.patchValue(response.userResponse)
      },
      error: () => {
      }
    })
  }

  async submit() {
    this.isLoading = true
    if(this.form.valid) {
      let currentLocation = await this.locationService.getSavedAddress(false)
      if(currentLocation)
        this.form.controls.location.setValue(currentLocation)

      this.accountService.onBoard(new User().deserialize(this.form.getRawValue()))
        .subscribe({
          next: (response) => {
            if(response) {
              this.commonService.showMessage(ONBOARDING_SUCCESS, MessageType.success)
              this.routingService.toMain()
            }
          },
          error: () => {
            this.isLoading = false
            this.commonService.showErrorMessage()
          },
          complete: () => this.isLoading = false
        })
    }
    else {    
      this.commonService.showErrorMessage(INVALID_INPUTS)
      this.form.markAllAsTouched()
      this.isLoading = false
    }
  }
}
