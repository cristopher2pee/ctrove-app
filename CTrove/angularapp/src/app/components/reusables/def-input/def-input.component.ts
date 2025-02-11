import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { NzButtonSize } from 'ng-zorro-antd/button';
import { DEFAULT_EMPTY_STRING } from 'src/Utilities/common/app-strings';

@Component({
  selector: 'app-def-input',
  templateUrl: './def-input.component.html',
  styleUrls: ['./def-input.component.css']
})
export class DefInputComponent {
  @Output() blur = new EventEmitter
  @Input() text: string = DEFAULT_EMPTY_STRING
  @Input() placeholder: string = DEFAULT_EMPTY_STRING
  @Input() control!: FormControl
  @Input() isDisabled: boolean = false
  @Input() size: NzButtonSize = 'default'
  @Input() type: 'color' | 'date' | 'datetime-local' | 'email' | 'month' | 'number' | 'password' | 'search' | 'tel' | 'text' | 'time' | 'url' | 'week' = 'text'
  @Input() max!: number
  @Input() min!: number
  @Input() mask!: string

  isRequired = () => this.control.hasValidator(Validators.required)
}
