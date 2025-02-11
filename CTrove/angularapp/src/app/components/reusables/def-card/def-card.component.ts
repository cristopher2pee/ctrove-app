import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-card',
  templateUrl: './def-card.component.html',
  styleUrls: ['./def-card.component.css']
})
export class DefCardComponent {
  @Input() title!: string
}
