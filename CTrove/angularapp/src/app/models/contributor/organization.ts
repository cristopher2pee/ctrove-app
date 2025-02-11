import { ADD, SEARCH, STATUS } from 'src/Utilities/common/app-strings';
import { Base, BaseFilter, Deserializable } from '../dto/common';
import { ContactType } from './contact-type';
import { Contributor } from './contributor';
import { VendorType } from './vendor-type';
import { Country } from './country';

export class Organization extends Base implements Deserializable {
  countryId!: string;
  country!: Country;
  companyName!: string;
  parent!: number;
  contactTypeId!: string;
  contactType!: ContactType;
  vendorTypeId!: string;
  vendorType!: VendorType;
  addressLine1!: string;
  addressLine2!: string;
  addressLine3!: string;
  zipCode!: string;
  city!: string;
  state!: string;
  phoneNumber!: string;
  faxNumber!: string;
  website!: string;
  primaryContactId!: string;
  primaryContributorContact!: Contributor;
  secondaryContactId!: string;
  secondaryContributorContact!: Contributor;
  email!: string;
  notes!: string;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export class OrganizationFilter extends BaseFilter implements Deserializable {
  countrysId!: string[];
  parentTypes!: number[];
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

// Fields
export const FIELDS = {
  search: SEARCH,
  name: 'Company Name',
  parent: 'Parent',
  contactType: 'Contact Type',
  vendorType: 'Vendor Type',
  country: 'Country',
  status: STATUS,
  primaryContact: 'Primary Contact',
  secondaryContact: 'Secondary Contact',
  email: 'Email',
  website: 'Website',
  state: 'State',
  city: 'City',
  zipCode: 'Zip Code',
  address1: 'Address 1',
  address2: 'Address 2',
  address3: 'Address 3',
  notes: 'Notes',
  phoneNumber: 'Phone Number',
  faxNumber: 'Fax Number',
  address: 'Address',
  contact: 'Contact',
  add: ADD
}
