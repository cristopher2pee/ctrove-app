import { ADD } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class Ethnicity extends Base implements Deserializable {
    blinded: boolean = false;
  
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}
  
export const ETHNICITY_FIELDS = {
    search: 'Search by name or code',
    status: 'Status',
    code: 'Code',
    name: 'Name',
    add: ADD,
}
  
export class EthnicityListFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}