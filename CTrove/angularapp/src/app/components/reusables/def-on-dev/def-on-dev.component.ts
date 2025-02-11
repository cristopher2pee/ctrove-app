import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-on-dev',
  templateUrl: './def-on-dev.component.html',
  styleUrls: ['./def-on-dev.component.css']
})
export class DefOnDevComponent {
  @Input() title: string = 'Content'
}
