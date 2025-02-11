import { ADD, ORGANIZATION, SEARCH, STATUS } from 'src/Utilities/common/app-strings';
import { Base, BaseFilter, Deserializable } from '../dto/common';
import { ContributorStudy } from './contributor-study';
import { Country } from './country';
import { Organization } from './organization';

export class Contributor extends Base implements Deserializable {
  objectId!: string;
  countryId!: string;
  country!: Country;
  email!: string;
  city!: string;
  prefix!: number;
  firstname!: string;
  lastname!: string;
  grade!: number;
  primaryJobTitle!: string;
  secondaryJobTitle!: string;
  phone!: string;
  mobile!: string;
  publicData!: boolean;
  organizationId!: string;
  organization!: Organization;
  active!: boolean;
  consent!: boolean;
  dateOfConsent!: Date;
  ContributorStudy!: ContributorStudy[];

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export class ContributorFilter extends BaseFilter implements Deserializable {
  countrysId!: string[];
  organizationsId!: string[];

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

// Fields
export const FIELDS = {
  search: SEARCH,
  prefix: 'Prefix',
  firstName: 'First Name',
  middleName: 'Middle Name',
  lastName: 'Last Name',
  grade: 'Grade',
  email: 'Email',
  mobileNo: 'Mobile Number',
  landLine: 'Landline',
  organization: ORGANIZATION,
  publicData: 'Public Data',
  consent: 'Consent',
  dateOfConsent: 'Date of Consent',
  studies: 'Studies',
  country: 'Country',
  city: 'City',
  primaryJob: 'Primary Job',
  secondaryJob: 'Secondary Job',
  status: STATUS,
  add: ADD
}

