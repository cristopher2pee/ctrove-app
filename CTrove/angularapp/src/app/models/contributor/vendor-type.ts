import { Base, BaseFilter, Deserializable } from '../dto/common';

export class VendorType extends Base implements Deserializable {
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export class VendorTypeFilter extends BaseFilter implements Deserializable {
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
