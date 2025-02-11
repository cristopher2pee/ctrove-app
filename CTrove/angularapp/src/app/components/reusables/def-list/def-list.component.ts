import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-def-list',
  templateUrl: './def-list.component.html',
  styleUrls: ['./def-list.component.css']
})
export class DefListComponent {
  @Output() itemClicked = new EventEmitter
  @Output() buttonClicked = new EventEmitter
  @Input() items!: any[]
  @Input() buttonText!: string
}
