import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-custom-select-field',
  templateUrl: './custom-select-field.component.html',
  styleUrls: ['./custom-select-field.component.css']
})
export class CustomSelectFieldComponent {
  @Output() onButtonClick = new EventEmitter
  @Output() selectionChanged = new EventEmitter
  @Output() onSearched = new EventEmitter
  @Output() onFocused = new EventEmitter
  @Input() control!: FormControl
  @Input() text!: string
  @Input() options!: any[]
  @Input() isDisabled: boolean = false
  @Input() hasButton: boolean = false
  @Input() buttonIcon: string = 'plus'

  isRequired = () => this.control.hasValidator(Validators.required)
}
