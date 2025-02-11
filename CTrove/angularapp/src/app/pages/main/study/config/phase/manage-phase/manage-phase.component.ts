import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { ADD_PHASE_SUCCESS, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_PHASE_SUCCESS } from 'src/Utilities/common/app-strings';
import { PHASE_FIELDS, Phase } from 'src/app/models/dto/phase';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { PhaseService } from 'src/app/services/main/config/phase.service';

@Component({
  selector: 'app-manage-phase',
  templateUrl: './manage-phase.component.html',
  styleUrls: ['./manage-phase.component.css']
})
export class ManagePhaseComponent extends BaseComponent implements OnInit, AfterViewInit {
  fields = PHASE_FIELDS
  isAdd: boolean = true
  data!: Phase | null
  phases!: Phase[]

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    prevPhase: new FormControl(),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  })

  constructor(private service: PhaseService,
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
            this.form.patchValue(this.data = response)  
            this.phases = this.phases.filter(d => d.id !== this.form.value.id)
          },
          error: () => {
            this.commonService.showErrorMessage()
            this.isLoading = false
          },
          complete: () => this.isLoading = false
        })
    }
  }

  ngAfterViewInit() {
    this.service.getDefList().subscribe({
      next: (response) => this.phases = response.data,
      error: () => this.commonService.showErrorMessage()
    })
  }

  async submit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        let d = new Phase().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(ADD_PHASE_SUCCESS)
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
              this.commonService.showMessage(UPDATE_PHASE_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              this.isLoading = false
              if(!this.data?.status && d.status != this.data?.status) 
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
