import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class Visit extends Base implements Deserializable {
    visitType!: number
    targetDays!: number
    timeWindow!: number
    visitId!: string
    isRequired: boolean = true
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const VISIT_FIELDS = {
    search: 'Search',
    code: 'Code',
    name: 'Name',
    visitType: 'Visit Type',
    targetDays: {
        name: 'Target Days',
        suffix: 'Day',
        suffixPlural: 'Days'
    },
    timeWindow: {
        name: 'Window Visit',
        suffix: 'Day',
        suffixPlural: 'Days'
    },
    visit: 'Visit',
    required: 'Required',
    status: STATUS,
    add: ADD
}

export class VisitFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}