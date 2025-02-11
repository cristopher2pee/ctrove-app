import { SAVE } from "src/Utilities/common/app-strings";
import { Base, Deserializable } from "./common"
import { TherapeuticArea } from "./therapeutic-area";
import { TrialClassification } from "./trial-classification";

export class Study extends Base implements Deserializable {
    sponsor!: string
    billingCode!: string
    studyType!: string
    classificationId!: string
    classification!: TrialClassification
    therapeuticArea!: TherapeuticArea
    therapeuticAreaId!: string


    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const STUDY_FIELDS = {
    search: 'Search',
    code: 'Study Code',
    name: 'Study Name',
    therapeuticArea: 'Therapeutic Area',
    studyType: 'Study Type',
    trialClassification: 'Trial Classification',
    billingCode: 'Billing Code',
    sponsor: 'Sponsor',
    remarks: 'Remarks',
    save: SAVE
}