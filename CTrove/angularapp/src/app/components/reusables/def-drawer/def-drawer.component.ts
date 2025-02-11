import { Component, Input } from '@angular/core';
import { NzDrawerPlacement } from 'ng-zorro-antd/drawer';

@Component({
  selector: 'app-def-drawer',
  templateUrl: './def-drawer.component.html',
  styleUrls: ['./def-drawer.component.css']
})
export class DefDrawerComponent {
  @Input() placement: NzDrawerPlacement = 'right'
}
