import { ADD } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";
import { Phase } from "./phase";
import { Ethnicity } from "./ethnicity";
import { Race } from "./race";
import { Site } from "./site";

export class Subject extends Base implements Deserializable {
    // blinded: boolean = false
    ethnicity!: Ethnicity
    ethnicityId!: string
    race!: Race
    raceId!: string
    randNo!: string
    screeningNo!: string
    sex!: number
    sites!: Site
    sitesId!: string
    subjectPhases!: SubjectPhase[]
    subjectStatus!: number
    yearOfBirth!: number

    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}

export class SubjectPhase extends Base implements Deserializable {
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

  
export const SUBJECT_FIELDS = {
    search: 'Search by name or code',
    status: 'Status',
    country: 'Country',
    site: 'Site',
    visit: 'Visit',
    screenNo: 'Screening No.',
    randomization: 'Randomization',
    yearOfBirth: 'Year of Birth',
    age: 'Age',
    sex: 'Sex',
    ethnicity: 'Ethnicity',
    race: 'Race',
    lastVisit: 'Last Visit',
    nextVisit: 'Next Visit',
    subjectStatus: 'Subject Status',
    dates: 'Start & End Date',
    phase: 'phase',
    addPhase: 'Add Phase',
    add: ADD,
}
  
export class SubjectFilter extends BaseFilter implements Deserializable {
    studyCountryIds!: string[]
    sitesIds!: string[]
    subjectStatus!: number[]

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}