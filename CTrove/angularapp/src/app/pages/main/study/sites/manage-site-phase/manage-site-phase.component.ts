import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_SITE_PHASE_SUCCESS } from 'src/Utilities/common/app-strings';
import { Phase } from 'src/app/models/dto/phase';
import { SITES_PHASE_FIELDS, SitePhase } from 'src/app/models/dto/site';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { PhaseService } from 'src/app/services/main/config/phase.service';
import { SitesService } from 'src/app/services/main/sites.service';

@Component({
  selector: 'app-manage-site-phase',
  templateUrl: './manage-site-phase.component.html',
  styleUrls: ['./manage-site-phase.component.css']
})
export class ManageSitePhaseComponent extends BaseModalComponent implements OnInit, AfterViewInit {
  fields = SITES_PHASE_FIELDS
  phases!: Phase[]
  addedPhases!: SitePhase[]
  override data!: SitePhase | null
  sitesId!: string | null

  form = new FormGroup({
    id: new FormControl(Guid.EMPTY),
    phaseId: new FormControl('', [Validators.required]),
    date: new FormControl(this.commonService.setDateRange(new Date, null)),
    sitesId: new FormControl(),
    startDate: new FormControl(new Date),
    endDate: new FormControl(new Date),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  })

  constructor(private service: SitesService,
    private phaseService: PhaseService,
    private modal: NzModalRef,
    private commonService: CommonService) {
    super();
  }

  ngOnInit() {

    // Set SitesId
    this.form.controls.sitesId.setValue(this.sitesId)

    if(!this.isAdd && this.data) {
      this.form.patchValue(this.data)

      if(this.data.id == Guid.EMPTY)
        return

      this.form.controls.remarks.setValue(null)
      this.form.controls.remarks.addValidators([Validators.required])
      this.service.getPhaseById(this.data.id)
        .subscribe({
          next: (response) => {
            if(!response)
              this.commonService.showErrorMessage()
            this.form.patchValue(this.data = response)
            this.form.controls.date.patchValue([this.data.startDate, this.data.endDate])
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
    this.form.controls.date.valueChanges.subscribe(response => {
      if(!response)
        return
      this.form.patchValue({
        startDate: response[0],
        endDate: response[1]
      })
    })

    this.phaseService.getDefList().subscribe({
      next: (response) => {
        if(!response) {
          this.commonService.showErrorMessage()
          return
        }
        this.phases = response.data.filter(d => !this.addedPhases.some(p => p.phaseId === d.id) || (!this.isAdd && d.id === this.data?.phaseId))
      },
      error: () => this.commonService.showErrorMessage()
    })
  }
  
  submit(isAdd: boolean) {
    if(isAdd) {
      if(this.form.valid) {
        let d = new SitePhase().deserialize(this.form.getRawValue())
        let p = this.phases.find(p => p.id === d.phaseId)
        if(p)
          d.phase = p

        if(this.isAdd || this.data?.id === Guid.EMPTY) {
          this.modal.destroy(d)
        }
        else {
          this.service.updatePhase(d).subscribe({
            next: (response) => {
              if(response) 
                this.commonService.showMessage(UPDATE_SITE_PHASE_SUCCESS)
            },
            error: () => this.commonService.showErrorMessage(),
            complete: () => {
              if(!d.status && d.status != this.data?.status && d.id !== Guid.EMPTY)
                this.service.removePhase(d).subscribe({
                  error: () => this.commonService.showErrorMessage(INACTIVATE_ERROR),
                  complete: () => this.modal.destroy(true)
                })
              else
                this.modal.destroy(true)
            }
          })
        }
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
