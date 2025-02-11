import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-icon',
  templateUrl: './def-icon.component.html',
  styleUrls: ['./def-icon.component.css']
})
export class DefIconComponent {
  @Input() icon!: string
}
