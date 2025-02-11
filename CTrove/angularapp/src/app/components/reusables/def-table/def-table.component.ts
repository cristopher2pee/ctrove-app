import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NzTableSize } from 'ng-zorro-antd/table';

@Component({
  selector: 'app-def-table',
  templateUrl: './def-table.component.html',
  styleUrls: ['./def-table.component.css']
})
export class DefTableComponent {
  @Output() dataClicked = new EventEmitter
  @Output() buttonClicked = new EventEmitter
  @Output() changed = new EventEmitter
  @Input() tableSize: NzTableSize = 'default'
  @Input() headers!: string[]
  @Input() dataSet!: any[]
  @Input() isLoading: boolean = false
  @Input() isBordered: boolean = false
  @Input() total!: number
  @Input() size!: number
  @Input() index!: number
  @Input() headerText!: string
  @Input() hasButton: boolean = false
  @Input() frontPagination: boolean = false
  @Input() isShowingSizeChanger: boolean = true
  @Input() sizeOptions: number[] = [5, 10, 20, 30, 40, 50]

  getValue = (data: any, req: number) => Object.values(data)[req]
}
