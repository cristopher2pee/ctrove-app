import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-def-number-input-group',
  templateUrl: './def-number-input-group.component.html',
  styleUrls: ['./def-number-input-group.component.css']
})
export class DefNumberInputGroupComponent {
  @Input() control!: FormControl
  @Input() prefix!: any
  @Input() suffix!: any
}
