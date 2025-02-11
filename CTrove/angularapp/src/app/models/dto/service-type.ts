import { ADD, STATUS } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class ServiceType extends Base implements Deserializable {
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}

export const SERVICE_TYPE_FIELDS = {
    search: 'Search',
    code: 'Code',
    name: 'Name',
    status: STATUS,
    add: ADD
}

export class ServiceTypeFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}