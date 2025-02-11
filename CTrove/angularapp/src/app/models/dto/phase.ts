import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class Phase extends Base implements Deserializable {
    prevPhase!: string
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const PHASE_FIELDS = {
    search: 'Search',
    code: 'Code',
    name: 'Name',
    prevPhase: 'Previous Phase',
    status: STATUS,
    add: ADD
}

export class PhaseFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}