import { Component, Input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-custom-number-input-field',
  templateUrl: './custom-number-input-field.component.html',
  styleUrls: ['./custom-number-input-field.component.css']
})
export class CustomNumberInputFieldComponent {
  @Input() control!: FormControl
  @Input() text!: string
  @Input() prefix!: any
  @Input() suffix!: any

  isRequired = () => this.control.hasValidator(Validators.required)
}
