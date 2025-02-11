import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-custom-date-range-picker-field',
  templateUrl: './custom-date-range-picker-field.component.html',
  styleUrls: ['./custom-date-range-picker-field.component.css']
})
export class CustomDateRangePickerFieldComponent {
  @Input() control!: FormControl
  @Input() text!: string
  @Input() isDisabled: boolean = false

  isRequired = () => this.control && this.control.hasValidator(Validators.required)
}
