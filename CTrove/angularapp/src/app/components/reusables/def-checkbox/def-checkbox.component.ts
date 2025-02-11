import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-def-checkbox',
  templateUrl: './def-checkbox.component.html',
  styleUrls: ['./def-checkbox.component.css']
})
export class DefCheckboxComponent {
  @Input() control = new FormControl
  @Input() isDisabled: boolean = false
}
