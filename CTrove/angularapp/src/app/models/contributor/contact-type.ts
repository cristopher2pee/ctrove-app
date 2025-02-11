import { Base, BaseFilter, Deserializable } from '../dto/common';

export class ContactType extends Base implements Deserializable {
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export class ContactTypeFilter extends BaseFilter implements Deserializable {
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
