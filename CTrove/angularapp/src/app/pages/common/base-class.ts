import { DEF_GUTTER } from "src/Utilities/common/app-variables"
import { DEFAULT_TABLE_META, Meta } from "src/app/models/dto/common"
import { environment } from "src/environments/environment"

export class BaseComponent {
    title: string = environment.appName
    isLoading: boolean = false
    isSideLoading: boolean = false
    defGutter = DEF_GUTTER
}

export class BaseMasterListComponent extends BaseComponent {
    override defGutter: number[] = [8, 8]
    headers!: string[]
    data!: any
    meta: Meta = DEFAULT_TABLE_META
    reset = () => this.meta = DEFAULT_TABLE_META
}

export class BaseModalComponent extends BaseComponent {
    isAdd: boolean = true
    hasChanged: boolean = false
    data!: any
    headers!: string[]
    meta: Meta = DEFAULT_TABLE_META
    reset = () => this.meta = DEFAULT_TABLE_META
}