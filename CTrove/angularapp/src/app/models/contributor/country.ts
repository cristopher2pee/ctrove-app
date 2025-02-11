import { Base, BaseFilter, Deserializable } from '../dto/common';

export class Country extends Base implements Deserializable {
  continent!: string;
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export class CountryFilter extends BaseFilter implements Deserializable {
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
