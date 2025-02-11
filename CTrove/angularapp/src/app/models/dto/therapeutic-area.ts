import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class TherapeuticArea extends Base implements Deserializable {
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const THERAPEUTIC_AREA_FIELDS = {
    search: 'Search',
    code: 'Therapeutic Area Code',
    name: 'Therapeutic Name',
    status: STATUS,
    add: ADD
}

export class TAListFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}