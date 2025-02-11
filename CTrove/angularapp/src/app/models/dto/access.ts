import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common"
import { User } from "./user";
import { StudySite } from "./study-site";
import { StudyCountry } from "./study-country";

export class Access extends Base implements Deserializable {
    userId!: string
    user!: User
    countryId!: string
    siteId!: string
    accessLevelId!: string | null
    rights!: number
    right!: number
    read!: boolean // = 1
    write!: boolean // = 2
    bin!: boolean // = 4

    setRights = () => 
    {
        if(this.read)
            this.rights = 1

        if(this.write)
            this.rights += 2

        if(this.bin)
            this.rights += 4
    }

    setAccessLevel = () => this.accessLevelId = this.siteId ? this.siteId : this.countryId ? this.countryId : null

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export interface CustomAccess {
    accessResponse: Access
    sitesResponse: StudySite
    studyCountry: StudyCountry
}

export class AccessListFilter extends BaseFilter implements Deserializable {
    right!: number
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

// Fields
export const ACCESS_FIELDS = {
    search: 'Search by name',
    user: 'User',
    studyCountry: 'Study Country',
    studySites: 'Study Sites',
    accessLevel: 'Access Level',
    rights: 'Rights',
    status: STATUS,
    read: 'Read',
    write: 'Write',
    bin: 'Bin',
    add: ADD
}
