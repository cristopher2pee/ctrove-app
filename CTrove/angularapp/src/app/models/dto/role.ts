import { Base, BaseFilter, Deserializable } from './common';
import { ADD } from 'src/Utilities/common/app-strings';

export class Role extends Base implements Deserializable {
  blinded: boolean = false;
  rolesPages!: RolesPage[];

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}

export const ROLES_FIELDS = {
  search: 'Search by name or code',
  pages: 'Pages',
  blinded: 'Blinded',
  status: 'Status',
  code: 'Code',
  name: 'Name',
  pageAccess: 'Page Access',
  add: ADD,
};

export interface RolesPage {
  id: string | null;
  rolesId: string | null;
  name: string;
  pages: number;
  status: boolean;
}

export class RolesListFilter extends BaseFilter implements Deserializable {
  blinded!: boolean;
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
