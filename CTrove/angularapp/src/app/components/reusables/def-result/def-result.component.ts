import { Component, Input } from '@angular/core';
import { NzResultStatusType } from 'ng-zorro-antd/result';

@Component({
  selector: 'app-def-result',
  templateUrl: './def-result.component.html',
  styleUrls: ['./def-result.component.css']
})
export class DefResultComponent {
  @Input() status: NzResultStatusType = 'success'
  @Input() title!: string
  @Input() subTitle!: string
  @Input() icon!: string
}
