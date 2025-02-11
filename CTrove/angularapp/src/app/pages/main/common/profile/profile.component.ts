import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { INVALID_INPUTS, PROFILE_UPDATE_REMARK, UPDATE_PROFILE_SUCCESS } from 'src/Utilities/common/app-strings';
import { USER_FIELDS, User } from 'src/app/models/dto/user';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { AccountService } from 'src/app/services/account.service';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { UserService } from 'src/app/services/main/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent extends BaseComponent implements OnInit {
  fields = USER_FIELDS

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    middlename: new FormControl(),
    suffix: new FormControl(),
    prefix: new FormControl(),
    email: new FormControl('', [Validators.required, Validators.email]),
    mobile: new FormControl('', [Validators.required]),
    landline: new FormControl(),
    organization: new FormControl('', [Validators.required]),
    rolesId: new FormControl(),
    remarks: new FormControl(PROFILE_UPDATE_REMARK)
  })

  constructor(private accountService: AccountService,
    private userService: UserService,
    private locationService: LocationService,
    private commonService: CommonService) {
    super();
  }

  ngOnInit() {
    this.isLoading = true
    this.accountService.getProfile()
      .subscribe({
        next: (response) => {
          this.form.patchValue(response.userResponse)
        },
        error: () => this.isLoading = false,
        complete: () => this.isLoading = false
      })
  }

  async submit() {
    if(this.form.valid) {
      this.isLoading = true
      var user = new User().deserialize(this.form.getRawValue())
      user.location = await this.locationService.getSavedAddress()
      this.userService.update(user).subscribe({
        next: (response) => {
          if(response)
            this.commonService.showMessage(UPDATE_PROFILE_SUCCESS)
        },
        error: () => {
          this.commonService.showErrorMessage()
          this.isLoading = false
        },
        complete: () => {
          this.isLoading = false
        }
      })
    }
    else {
      this.form.markAllAsTouched()
      this.commonService.showErrorMessage(INVALID_INPUTS)
    }
  }

  onBlur(req: number, val: any) {
    switch(req) {
      case 1: {
        if(!val)
          return
        this.userService.validateEmail(val).subscribe({
          next: (response) => {
            if(response)
              this.form.controls.email.setErrors({ 'email-exist' : true })
          },
          error: () => this.commonService.showErrorMessage()
        })
      }
      break;
    }
  }
}
