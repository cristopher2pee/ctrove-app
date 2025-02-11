import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-text',
  templateUrl: './def-text.component.html',
  styleUrls: ['./def-text.component.css']
})
export class DefTextComponent {
  @Input() h!: HeaderType
  @Input() type!: "secondary" | "warning" | "danger" | "success"
  @Input() fontType: 'light' | 'regular' | 'medium' | 'bold' = 'regular'
}

enum HeaderType {
  h1 = 1,
  h2 = 2,
  h3 = 3,
  h4 = 4,
  h5 = 5,
  h6 = 6,
}