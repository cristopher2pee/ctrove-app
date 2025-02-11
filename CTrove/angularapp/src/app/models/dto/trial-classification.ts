import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class TrialClassification extends Base implements Deserializable {
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const TRIAL_CLASSIFICATION_FIELD = {
    search: 'Search',
    code: 'Code',
    name: 'Name',
    status: STATUS,
    add: ADD
}

export class TCFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}