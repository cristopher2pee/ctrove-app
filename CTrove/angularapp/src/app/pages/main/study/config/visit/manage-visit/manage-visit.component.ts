import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { VISIT_TYPE_OPTIONS } from 'src/Utilities/common/app-data';
import { ADD_VISIT_SUCCESS, INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_VISIT_SUCCESS } from 'src/Utilities/common/app-strings';
import { VISIT_FIELDS, Visit } from 'src/app/models/dto/visit';
import { BaseComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { LocationService } from 'src/app/services/common/location.service';
import { VisitService } from 'src/app/services/main/config/visit.service';

@Component({
  selector: 'app-manage-visit',
  templateUrl: './manage-visit.component.html',
  styleUrls: ['./manage-visit.component.css']
})
export class ManageVisitComponent extends BaseComponent implements OnInit, AfterViewInit {
  isAdd: boolean = true
  data?: Visit | null
  fields = VISIT_FIELDS
  visits!: any[]
  visitTypeOptions = VISIT_TYPE_OPTIONS

  form = new FormGroup({
    id: new FormControl(Guid.create().toString()),
    code: new FormControl('', [Validators.required]),
    name: new FormControl('',[Validators.required]),
    visitType: new FormControl(1),
    targetDays: new FormControl(0),
    timeWindow: new FormControl(0),
    visitId: new FormControl(Guid.create().toString()),
    isRequired: new FormControl(true),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  })

  constructor(private service: VisitService,
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
            this.visits = this.visits.filter(d => d.id !== this.form.value.id)  
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
      next: (response) => {
        if(!response)
          this.commonService.showErrorMessage()
        this.visits = response.data
      },
      error: () => this.commonService.showErrorMessage()
    })

    this.form.controls.visitType.valueChanges.subscribe(val => {
      if(val === 2) 
        this.form.patchValue({
          targetDays: 0,
          timeWindow: 0,
          visitId: Guid.EMPTY,
          isRequired: false
        })
       
    })
  }

  async submit(isSave: boolean) {
    if(isSave) {
      if(this.form.valid) {
        this.isLoading = true
        let d = new Visit().deserialize(this.form.getRawValue())
        d.location = await this.locationService.getSavedAddress()
        if(this.isAdd) 
          this.service.save(d).subscribe({
            next: (response) => {
              if(!response)
                this.commonService.showErrorMessage()
              this.commonService.showMessage(ADD_VISIT_SUCCESS)
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
              this.commonService.showMessage(UPDATE_VISIT_SUCCESS)
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
