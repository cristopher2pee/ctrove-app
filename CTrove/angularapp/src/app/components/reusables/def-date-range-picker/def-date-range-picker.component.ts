import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-def-date-range-picker',
  templateUrl: './def-date-range-picker.component.html',
  styleUrls: ['./def-date-range-picker.component.css']
})
export class DefDateRangePickerComponent {
  @Input() control!: FormControl
}
