import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { CommonService } from 'src/app/services/common/common.service';

@Component({
  selector: 'app-custom-select-status',
  templateUrl: './custom-select-status.component.html',
  styleUrls: ['./custom-select-status.component.css']
})
export class CustomSelectStatusComponent {
  @Output() selectionChanged = new EventEmitter
  @Input() control!: FormControl
  @Input() placeholder: string = 'Choose'
  @Input() isDisabled: boolean = false

  constructor(public commonService: CommonService) { }

  onChanged(val: any) {
    this.control.setValue(val)
    this.selectionChanged.emit(val)
  }
}
