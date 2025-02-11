import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ADD_RACE_SUCCESS, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_RACE_SUCCESS } from 'src/Utilities/common/app-strings';
import { RACE_FIELDS, Race } from 'src/app/models/dto/race';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { RaceService } from 'src/app/services/main/config/race.service';

@Component({
  selector: 'app-manage-race',
  templateUrl: './manage-race.component.html',
  styleUrls: ['./manage-race.component.css']
})
export class ManageRaceComponent extends BaseComponent implements OnInit {
  fields = RACE_FIELDS
  isAdd: boolean = true
  data!: Race | null

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    code: new FormControl('', [Validators.required]),
    name: new FormControl('',[Validators.required]),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  })

  constructor(private service: RaceService,
    private modal: NzModalRef,
    private commonService: CommonService,
    private locationService: LocationService) {
    super();
  }

  ngOnInit() {
    if(!this.isAdd && this.data) {
      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.service.getById(this.data.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.form.patchValue(response)  
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = false
        })
    }
  }

  async submit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        let d = new Race().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(ADD_RACE_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              this.isLoading = false
              this.modal.destroy(true)
            }
          })
        else
          this.service.update(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(UPDATE_RACE_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              this.isLoading = false
              if(!d.status && d.status != this.data?.status) 
              this.service.remove(d).subscribe({
                error: () => this.commonService.showErrorMessage(INACTIVATE_ERROR)
              })
              this.modal.destroy(true)
            }
          })
      }
      else {
        this.form.markAllAsTouched()
        this.commonService.showErrorMessage(INVALID_INPUTS)
      }
    }
    else 
      this.modal.destroy()
  }
}
