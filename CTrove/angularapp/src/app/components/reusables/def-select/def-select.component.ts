import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { NzSelectModeType, NzSelectSizeType } from 'ng-zorro-antd/select';

@Component({
  selector: 'app-def-select',
  templateUrl: './def-select.component.html',
  styleUrls: ['./def-select.component.css']
})
export class DefSelectComponent {
  @Output() selectionChanged = new EventEmitter
  @Output() onSearched = new EventEmitter
  @Output() onFocused = new EventEmitter

  @Input() mode: NzSelectModeType = 'default'
  @Input() control!: FormControl
  @Input() options!: any[]
  @Input() placeholder: string = 'Choose'
  @Input() isDisabled: boolean = false
  @Input() allowClear: boolean = true
  @Input() size: NzSelectSizeType = 'default'
  nzFilterOption = (): boolean => true;

  changed(val: any) {
    this.control.setValue(val)
    this.selectionChanged.emit(val)
  }
}
