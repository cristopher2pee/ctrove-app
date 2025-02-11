import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageType } from 'src/Utilities/common/app-enums';
import { INVITE_SUCCESS, SOMETHING_WENT_WRONG } from 'src/Utilities/common/app-strings';
import { Role } from 'src/app/models/dto/role';
import { User } from 'src/app/models/dto/user';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { ResourceService } from 'src/app/services/common/resource.service';
import { UserService } from 'src/app/services/main/user.service';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.css']
})
export class InviteComponent extends BaseComponent implements OnInit {
  rolesOptions!: Role[]

  form = new FormGroup({
    email: new FormControl('', [Validators.required]),
    rolesId: new FormControl('', [Validators.required]),
  })

  constructor(private userService: UserService,
    private resourceService: ResourceService,
    private commonService: CommonService) {
    super()
  }

  ngOnInit() {
    this.resourceService.getInviteResource()
      .subscribe({
        next: (response) => {
          this.rolesOptions = response as Role[]
        },
        error: () => {

        },
        complete: () => {

        }
      })
  }

  submit() {
    this.isLoading = true
    if(this.form.valid) {
      this.userService.invite(new User().deserialize(this.form.getRawValue()))
        .subscribe({
          next: (response) => {
            if(response)
              this.commonService.showMessage(INVITE_SUCCESS, MessageType.success)
          },
          error: () => {
            this.isLoading = false
          },
          complete: () => {
            this.form.reset()
            this.isLoading = false
          }
        })
    }
    else {
      this.commonService.showErrorMessage(SOMETHING_WENT_WRONG)
      this.form.markAllAsTouched()
    }
  }
}
