import { Base, Deserializable } from "./common";
import { StudyCountry } from "./study-country";

export class StudySite extends Base implements Deserializable {
    studyCountryId!: string
    studyCountry!: StudyCountry
    siteStatus!: number
    startDate!: Date
    endDate!: Date

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}