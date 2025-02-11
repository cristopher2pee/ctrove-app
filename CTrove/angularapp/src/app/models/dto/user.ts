import { ADD, EDIT, PROCEED, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";
import { Role } from "./role";
import { Access } from "./access";
import { Site } from "./site";
import { StudyCountry } from "./study-country";

export class User extends Base implements Deserializable {
    prefix!: string
    suffix!: string
    firstname!: string
    lastname!: string
    middlename!: string
    email!: string
    mobile!: string
    landline!: string
    startDate!: Date
    endDate!: Date
    organization!: string
    rolesId!: string
    roles!: Role

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export interface AccountProfile {
    userResponse: User,
    accessListResponse: Access[],
    sitesListResponse: Site[]
    studyCountryListResponse: StudyCountry[]
} 

export class UserListFilter extends BaseFilter implements Deserializable {
    rolesId!: string[]
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

// Fields
export const USER_FIELDS = {
    headerPersonal: 'Personal Information',
    headerContact: 'Contact Information',
    search: 'Search by email, first or last name',
    prefix: {
        name: 'Prefix',
        maxLength: 5
    },
    firstName: {
        name: 'First Name',
        maxLength: 50
    },
    lastName: {
        name: 'Last Name',
        maxLength: 50
    },
    middleName: {
        name: 'Middle Name',
        maxLength: 50
    },
    suffix: {
        name: 'Suffix',
        maxLength: 5
    },
    email: 'Email',
    mobileNumber: {
        name: 'Mobile No.',
        mask: '(000) 000 - 0000'
    },
    landline: {
        name: 'Landline',
        mask: '(000) 000 - 0000'
    },
    startDate: 'Start Date',
    endDate: 'End Date',
    organization: {
        name: 'Organization',
        maxLength: 50
    },
    roles: 'Roles',
    status: STATUS,
    dates: 'Start & End Date',
    location: 'Location',
    add: ADD,
    edit: EDIT,
    proceed: PROCEED
}