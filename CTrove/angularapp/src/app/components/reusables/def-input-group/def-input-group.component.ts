import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DEFAULT_EMPTY_STRING } from 'src/Utilities/common/app-strings';

@Component({
  selector: 'app-def-input-group',
  templateUrl: './def-input-group.component.html',
  styleUrls: ['./def-input-group.component.css']
})
export class DefInputGroupComponent {
  @Input() control!: FormControl
  @Input() placeholder: string = DEFAULT_EMPTY_STRING
  @Input() icon!: string
}
