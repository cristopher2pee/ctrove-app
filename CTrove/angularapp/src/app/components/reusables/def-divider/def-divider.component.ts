import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-divider',
  templateUrl: './def-divider.component.html',
  styleUrls: ['./def-divider.component.css']
})
export class DefDividerComponent {
  @Input() type: 'horizontal' | 'vertical' = 'horizontal'
  @Input() text!: string
  @Input() icon!: string
  @Input() orientation: 'center' | 'left' | 'right' = 'center'
  @Input() isPlain: boolean = true
}
