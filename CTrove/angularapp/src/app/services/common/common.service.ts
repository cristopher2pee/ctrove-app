import { DatePipe } from '@angular/common';
import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NzMessageService } from 'ng-zorro-antd/message';
import { GENDER_OPTIONS, GRADE_TYPE_OPTIONS, PAGE_OPTIONS, PARENT_TYPE_OPTIONS, PREFIX_TYPE_OPTIONS, SITE_STATUS_OPTIONS, SUBJECT_STATUS_OPTIONS } from 'src/Utilities/common/app-data';
import { MessageType } from 'src/Utilities/common/app-enums';
import { NO_DATA, SOMETHING_WENT_WRONG } from 'src/Utilities/common/app-strings';
import { ACCESS_FIELDS } from 'src/app/models/dto/access';
import { RolesPage } from 'src/app/models/dto/role';
import { User } from 'src/app/models/dto/user';
import { environment } from 'src/environments/environment';
import timezones from 'timezones-list';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private messagingService: NzMessageService,
    private datePipe: DatePipe,
    private titleService: Title) { }

  // Browser
  setTitle = (title: string) => this.titleService.setTitle(`${environment.appName} - ${title}`)

  // Message
  showMessage = (message: string, type: MessageType = MessageType.success, duration: number = 3000) => this.messagingService.create(type, message, { nzDuration: duration })

  showErrorMessage = (message: string = SOMETHING_WENT_WRONG) => this.showMessage(message, MessageType.error)

  // Number
  getTotalRights = (hasRead: boolean | null | undefined, hasWrite: boolean | null | undefined, hasBin: boolean | null | undefined) => {
    let total = 0

    if(hasRead)
      total += 1
    
    if(hasWrite)
      total += 2

    if(hasBin)
      total += 4

    return total
  }

  // String
  capitalizeFirstLetter = (str: string) : string => str.charAt(0).toUpperCase() + str.slice(1)

  formatGender = (id: number) => GENDER_OPTIONS.find(d => d.id === id)?.name

  formatSubjectStatus = (id: number) => SUBJECT_STATUS_OPTIONS.find(d => d.id === id)?.name

  formatParentType = (id: number) => PARENT_TYPE_OPTIONS.find(d => d.id === id)?.name

  formatPrefixType = (id: number) => PREFIX_TYPE_OPTIONS.find(d => d.id === id)?.name

  formatGradeType = (id: number) => GRADE_TYPE_OPTIONS.find(d => d.id === id)?.name

  formatStatus = (status: boolean) => status ? 'Active' : 'Inactive'

  formatBlinded = (isBlinded: boolean) => isBlinded ? 'Blinded' : 'Unblinded'

  formatRights = (rights: number) => 
    rights === 1 ? ACCESS_FIELDS.read : 
    rights === 3 ? `${ACCESS_FIELDS.read}, ${ACCESS_FIELDS.write}` : 
    rights === 5 ? `${ACCESS_FIELDS.read}, ${ACCESS_FIELDS.bin}` : 
    rights === 7 ? `${ACCESS_FIELDS.read}, ${ACCESS_FIELDS.write}, ${ACCESS_FIELDS.bin}` : 
    null

  formatSiteStatus = (id: number) => SITE_STATUS_OPTIONS.find(d => d.id === id)?.name

  formatName(e: User) {
    if(!e)
      return NO_DATA

    let result: string
    try {
      if(e.lastname) {
        result = `${ e.lastname }, ${ e.firstname }`
        if(e.middlename)
          result += ` ${ e.middlename.charAt(0) }.`
      }
      else
        result = e.firstname
    } catch (error) {
      result = `${ e.lastname }, ${ e.middlename }`
    }
    return result;
  }

  formatNames = (fn: string, ln: string) => `${ln}, ${fn}`

  formatPageAccess = (pages: RolesPage[]) => pages.map(d => PAGE_OPTIONS.find(p => p.pages === d.pages)?.name).join(', ')
  // Date
  formatDate = (date: Date) => this.datePipe.transform(date, 'MM/dd/yyyy')

  setDateRange = (start: Date = new Date, end: Date | null = new Date) => end ? [start, end] : [start]

  getDate = (yearOffset: number = 0) => {
    let date = new Date

    date.setFullYear(date.getFullYear() - yearOffset)

    return date
  }

  // Array
  getTimezones = () => timezones
    .map(t => ({
      id: t.tzCode,
      name: t.label
    }))

  // Morph
  morphArray(arr: any[], n: number) {
    var result = arr
    for(var i = 0; i < n; i++)
      result = result.concat(arr)
    return result
  }
}
