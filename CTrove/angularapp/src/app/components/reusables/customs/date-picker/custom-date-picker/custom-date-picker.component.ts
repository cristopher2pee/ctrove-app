import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-custom-date-picker',
  templateUrl: './custom-date-picker.component.html',
  styleUrls: ['./custom-date-picker.component.css']
})
export class CustomDatePickerComponent {
  @Input() control!: FormControl
  @Input() text!: string
  @Input() isDisabled: boolean = false
  @Input() mode: 'week' | 'month' | 'year' = 'year'

  isRequired = () => this.control && this.control.hasValidator(Validators.required)
}
