import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzAnchorModule } from 'ng-zorro-antd/anchor';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';

// Components
import { ErrorComponent } from 'src/app/components/errors/error/error.component';
import { DefButtonComponent } from 'src/app/components/reusables/def-button/def-button.component';
import { DefInputComponent } from 'src/app/components/reusables/def-input/def-input.component';
import { DefAvatarComponent } from 'src/app/components/reusables/def-avatar/def-avatar.component';
import { DefIconComponent } from 'src/app/components/reusables/def-icon/def-icon.component';
import { DefResultComponent } from '../../components/reusables/def-result/def-result.component';
import { NzResultModule } from 'ng-zorro-antd/result';
import { DefCardComponent } from '../../components/reusables/def-card/def-card.component';
import { DefDateRangePickerComponent } from '../../components/reusables/def-date-range-picker/def-date-range-picker.component';
import { DefOnDevComponent } from '../../components/reusables/def-on-dev/def-on-dev.component';
import { DefLayoutComponent } from '../../components/reusables/def-layout/def-layout.component';
import { DefUserProfileComponent } from '../../components/reusables/def-user-profile/def-user-profile.component';
import { DefContentComponent } from '../../components/reusables/def-content/def-content.component';
import { DefImageComponent } from '../../components/reusables/def-image/def-image.component';
import { DefTextComponent } from '../../components/reusables/typo/def-text/def-text.component';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { DefDividerComponent } from '../../components/reusables/def-divider/def-divider.component';
import { DefCheckboxComponent } from '../../components/reusables/def-checkbox/def-checkbox.component';
import { DefTableComponent } from '../../components/reusables/def-table/def-table.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { DefSelectComponent } from '../../components/reusables/def-select/def-select.component';
import { DefSkeletonComponent } from '../../components/reusables/def-skeleton/def-skeleton.component';
import { DefInputGroupComponent } from '../../components/reusables/def-input-group/def-input-group.component';
import { CustomSelectFieldComponent } from 'src/app/components/reusables/customs/select/custom-select-field/custom-select-field.component';
import { CustomSelectStatusComponent } from '../../components/reusables/customs/select/custom-select-status/custom-select-status.component';
import { DefDrawerComponent } from '../../components/reusables/def-drawer/def-drawer.component';
import { CustomDateRangePickerFieldComponent } from '../../components/reusables/customs/date-picker/custom-date-range-picker-field/custom-date-range-picker-field.component';
import { CustomModalFooterComponent } from '../../components/reusables/customs/custom-modal-footer/custom-modal-footer.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { DefLoadingScreenComponent } from '../../components/reusables/def-loading-screen/def-loading-screen.component';
import { DefNumberInputGroupComponent } from 'src/app/components/reusables/def-number-input-group/def-number-input-group.component';
import { DefSkeletonElementComponent } from 'src/app/components/reusables/def-skeleton-element/def-skeleton-element.component';
import { AuditTrailComponent } from '../../pages/common/drawers/audit-trail/audit-trail.component';
import { DefDatePickerComponent } from '../../components/reusables/def-date-picker/def-date-picker.component';
import { CustomDatePickerComponent } from '../../components/reusables/customs/date-picker/custom-date-picker/custom-date-picker.component';
import { CustomSitesSwitcherComponent } from 'src/app/components/reusables/customs/custom-sites-switcher/custom-sites-switcher.component';
import { CustomInputFieldComponent } from 'src/app/components/reusables/customs/input/custom-input-field/custom-input-field.component';
import { CustomNumberInputFieldComponent } from 'src/app/components/reusables/customs/input/custom-number-input-field/custom-number-input-field.component';
import { DefListComponent } from '../../components/reusables/def-list/def-list.component';
import { DefTextareaComponent } from '../../components/reusables/def-textarea/def-textarea.component';
import { CustomTextareaComponent } from '../../components/reusables/customs/input/custom-textarea/custom-textarea.component';

@NgModule({
  declarations: [
    ErrorComponent,
    DefButtonComponent,
    DefInputComponent,
    DefAvatarComponent,
    DefIconComponent,
    DefResultComponent,
    DefCardComponent,
    DefDateRangePickerComponent,
    DefOnDevComponent,
    DefLayoutComponent,
    DefUserProfileComponent,
    DefContentComponent,
    DefImageComponent,
    DefTextComponent,
    DefDividerComponent,
    CustomInputFieldComponent,
    DefCheckboxComponent,
    DefTableComponent,
    DefSelectComponent,
    CustomSelectFieldComponent,
    DefSkeletonComponent,
    DefInputGroupComponent,
    CustomSelectStatusComponent,
    DefDrawerComponent,
    CustomDateRangePickerFieldComponent,
    CustomModalFooterComponent,
    DefLoadingScreenComponent,
    DefNumberInputGroupComponent,
    DefSkeletonElementComponent,
    AuditTrailComponent,
    DefDatePickerComponent,
    CustomDatePickerComponent,
    CustomSitesSwitcherComponent,
    CustomNumberInputFieldComponent,
    DefListComponent,
    DefTextareaComponent,
    CustomTextareaComponent
  ],
  imports: [
    CommonModule,
    NzButtonModule,
    NzIconModule,
    NzInputModule,
    NzAvatarModule,
    NzResultModule,
    NzCardModule,
    NzDatePickerModule,
    NzLayoutModule,
    NzDropDownModule,
    NzPopconfirmModule,
    NzModalModule,
    NzImageModule,
    NzTypographyModule,
    NzDividerModule,
    NzCheckboxModule,
    NzTableModule,
    NzFormModule,
    ReactiveFormsModule,
    NzGridModule,
    NzSelectModule,
    NzSkeletonModule,
    FormsModule,
    NzDrawerModule,
    NgxMaskDirective, 
    NgxMaskPipe,
    NzBadgeModule,
    NzSpinModule,
    NzCollapseModule,
    NzAnchorModule,
    NzInputNumberModule,
    NzMessageModule,
    NzTimelineModule,
    NzEmptyModule,
    NzCarouselModule,
    NzTagModule,
    NzTabsModule,
    NzListModule
  ],
  exports: [
    ErrorComponent,
    DefButtonComponent,
    DefInputComponent,
    DefAvatarComponent,
    DefIconComponent,
    DefResultComponent,
    DefCardComponent,
    DefDateRangePickerComponent,
    DefOnDevComponent,
    DefLayoutComponent,
    DefUserProfileComponent,
    DefContentComponent,
    DefImageComponent,
    DefTextComponent,
    DefDividerComponent,
    DefCheckboxComponent,
    DefTableComponent,
    CustomInputFieldComponent,
    DefSelectComponent,
    CustomSelectFieldComponent,
    DefSkeletonComponent,
    NzGridModule, 
    DefInputGroupComponent,
    CustomSelectStatusComponent,
    DefDrawerComponent,
    CustomDateRangePickerFieldComponent,
    CustomModalFooterComponent,
    NzSkeletonModule,
    DefLoadingScreenComponent,
    NzImageModule,
    NzCollapseModule,
    ReactiveFormsModule,
    NzAnchorModule,
    DefNumberInputGroupComponent,
    CustomNumberInputFieldComponent,
    DefSkeletonElementComponent,
    NzLayoutModule,
    NzMessageModule,
    AuditTrailComponent,
    NzCarouselModule,
    NzTagModule,
    NzButtonModule,
    NzIconModule,
    DefDatePickerComponent,
    CustomDatePickerComponent,
    CustomSitesSwitcherComponent,
    NzTabsModule,
    NzListModule,
    DefListComponent,
    DefTextareaComponent,
    CustomTextareaComponent
  ],
  providers: [
    provideNgxMask()
  ]
})
export class ReusableModule { }
