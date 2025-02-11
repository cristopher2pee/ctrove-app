import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { INACTIVATE_ERROR, INVALID_INPUTS, NEW_RECORD, UPDATE_SUBJECT_PHASE_SUCCESS } from 'src/Utilities/common/app-strings';
import { Phase } from 'src/app/models/dto/phase';
import { SUBJECT_FIELDS, SubjectPhase } from 'src/app/models/dto/subject';
import { BaseModalComponent } from 'src/app/pages/common/base-class';
import { CommonService } from 'src/app/services/common/common.service';
import { PhaseService } from 'src/app/services/main/config/phase.service';
import { SubjectService } from 'src/app/services/main/subject.service';

@Component({
  selector: 'app-manage-subject-phase',
  templateUrl: './manage-subject-phase.component.html',
  styleUrls: ['./manage-subject-phase.component.css']
})
export class ManageSubjectPhaseComponent extends BaseModalComponent implements OnInit, AfterViewInit {
  fields = SUBJECT_FIELDS
  phases!: Phase[]
  addedPhases!: SubjectPhase[]
  override data!: SubjectPhase | null

  form = new FormGroup({
    id: new FormControl(Guid.EMPTY),
    phaseId: new FormControl('', [Validators.required]),
    date: new FormControl(this.commonService.setDateRange()),
    subjectId: new FormControl(),
    startDate: new FormControl(new Date),
    endDate: new FormControl(new Date),
    status: new FormControl(true),
    remarks: new FormControl(NEW_RECORD)
  })

  constructor(private service: SubjectService,
    private phaseService: PhaseService,
    private modal: NzModalRef,
    private commonService: CommonService) {
    super();
  }

  ngOnInit() {
    if(!this.isAdd && this.data) {
      this.form.patchValue(this.data)
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
        let d = new SubjectPhase().deserialize(this.form.getRawValue())
        let p = this.phases.find(p => p.id === d.phaseId)
        if(p)
          d.phase = p

        if(this.isAdd) {
          this.modal.destroy(d)
        }
        else {
          this.service.updatePhase(d).subscribe({
            next: (response) => {
              if(response) 
                this.commonService.showMessage(UPDATE_SUBJECT_PHASE_SUCCESS)
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
