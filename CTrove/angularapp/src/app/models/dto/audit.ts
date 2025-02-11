import { Base, Deserializable } from "./common";
import { User } from "./user";

export class Audit extends Base implements Deserializable {
    performedBy!: string
    userResponse!: User
    fullName!: string
    datePeformed!: Date
    fromValue!: string
    toValue!: string
    affectedColumn!: string
    recordId!: string
    table!: string
    auditType!: number

    deserialize(input: any): this {
        Object.assign(this, input);
        return this;
    }
}