import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-def-skeleton',
  templateUrl: './def-skeleton.component.html',
  styleUrls: ['./def-skeleton.component.css']
})
export class DefSkeletonComponent {
  count = []

  constructor() {
    this.count.length = 1
  }

  @Input() set setCount (c: number) { 
    this.count.length = c
  }
}
