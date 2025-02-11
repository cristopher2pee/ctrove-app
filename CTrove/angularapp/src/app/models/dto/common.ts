import { CANCEL, EDIT, SAVE } from "src/Utilities/common/app-strings";

export interface Deserializable {
    deserialize(input: any): this;
}

export class Base {
    id!: string
    status: boolean = true
    code!: string
    name!: string
    remarks!: string
    location!: string
}

export class BaseFilter {
    search!: string
    status: boolean = false
}

export interface ApiResponse<T>{
    data: T
}

export interface Meta {
    total: number,
    limit: number,
    page: number,
    lastPage: number,
}

export const DEFAULT_TABLE_META: Meta = {
    total: 0,
    limit: 10,
    page: 1,
    lastPage: 0
}

export const DEFAULT_SIDE_TABLE_META: Meta = {
    total: 0,
    limit: 5,
    page: 1,
    lastPage: 0
}

export interface PageList<T>{
    data: T[],
    meta: Meta
}

export const BIN_FIELDS = {
    remarks: 'Remarks',
    save: SAVE,
    edit: EDIT,
    cancel: CANCEL,
}