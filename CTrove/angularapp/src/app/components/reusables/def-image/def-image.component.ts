import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-image',
  templateUrl: './def-image.component.html',
  styleUrls: ['./def-image.component.css']
})
export class DefImageComponent {
  @Input() source!: string
}
