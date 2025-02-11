import { ADD, SAVE, STATUS } from "src/Utilities/common/app-strings"
import { Base, BaseFilter, Deserializable } from "./common"
import { Study } from "./study"
import { ServiceType } from "./service-type"
import { Phase } from "./phase"

export class Site extends Base implements Deserializable {
    studyCountryId!: string
    serviceTypeId!: string
    siteStatus!: number
    startDate!: Date
    endDate!: Date
    studyCountry!: Study
    serviceType!: ServiceType
    sitePhases!: SitePhase[]

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const SITES_FIELDS = {
    search: 'Search',
    status: STATUS,
    serviceType: 'Service Type',
    studyCountry: 'Study Country',
    code: 'Site Code',
    name: 'Site Name',
    startDate: 'Start Date',
    endDate: 'End Date',
    siteStatus: 'Site Status',
    phase: 'Phase',
    dates: 'Start & End Date',
    addPhase: 'Add Phase',
    save: SAVE,
    add: ADD
}

export class SitePhase extends Base implements Deserializable {
    phaseId!: string
    sitesId!: string
    phase!: Phase
    startDate!: Date
    endDate!: Date

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const SITES_PHASE_FIELDS = {
    phase: 'Phase',
    dates: 'Start & End Date',
}


export class SitesFilter extends BaseFilter implements Deserializable {
    siteStatusId!: string[]
    studyCountryId!: string[]
    serviceTypeId!: string[]

    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}