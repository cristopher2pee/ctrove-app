import { Base, Deserializable } from '../dto/common';

export class ContributorStudy extends Base implements Deserializable {
  studyId!: string;
  studyName!: string;
  contributorId!: string;
  sponsorId!: string;
  role!: string;
  startDate!: Date;
  endDate!: Date;
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export const FIELDS = {
  studyName: 'Study Name',
  sponsor: 'Sponsor',
  role: 'Role',
  startDate: 'Start Date',
  endDate: 'End Date'
}
