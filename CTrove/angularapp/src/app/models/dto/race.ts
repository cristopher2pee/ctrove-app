import { ADD } from "src/Utilities/common/app-strings";
import { Base, BaseFilter, Deserializable } from "./common";

export class Race extends Base implements Deserializable {
    blinded: boolean = false;
  
    deserialize(input: any): this {
      Object.assign(this, input);
      return this;
    }
}
  
export const RACE_FIELDS = {
    search: 'Search by name or code',
    status: 'Status',
    code: 'Code',
    name: 'Name',
    add: ADD,
}
  
export class RaceListFilter extends BaseFilter implements Deserializable {
    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}