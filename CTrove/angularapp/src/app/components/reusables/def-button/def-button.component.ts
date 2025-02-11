import { Component, Input } from '@angular/core';
import { NzButtonShape, NzButtonSize, NzButtonType } from 'ng-zorro-antd/button';

@Component({
  selector: 'app-def-button',
  templateUrl: './def-button.component.html',
  styleUrls: ['./def-button.component.css']
})
export class DefButtonComponent {
  @Input() text!: string
  @Input() type: NzButtonType = 'primary'
  @Input() size: NzButtonSize = 'default'
  @Input() shape!: NzButtonShape
  @Input() icon!: string
  @Input() isLoading: boolean = false
  @Input() isDisabled: boolean = false
  @Input() isDanger: boolean = false
  @Input() isBlock: boolean = false
}
