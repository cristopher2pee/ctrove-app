import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StudyRoutingModule } from './study-routing.module';
import { SubjectsComponent } from 'src/app/pages/main/study/subjects/subjects.component';
import { SitesComponent } from 'src/app/pages/main/study/sites/sites.component';
import { ConfigComponent } from 'src/app/pages/main/study/config/config.component';
import { ReusableModule } from '../../reusable/reusable.module';
import { TherapeuticAreaComponent } from '../../../pages/main/study/config/therapeutic-area/therapeutic-area.component';
import { ManageTherapeuticAreaComponent } from '../../../pages/main/study/config/therapeutic-area/manage-therapeutic-area/manage-therapeutic-area.component';
import { TrialClassificationComponent } from '../../../pages/main/study/config/trial-classification/trial-classification.component';
import { ManageTrialClassificationComponent } from '../../../pages/main/study/config/trial-classification/manage-trial-classification/manage-trial-classification.component';
import { StudyCountryComponent } from '../../../pages/main/study/config/study-country/study-country.component';
import { ManageStudyCountryComponent } from '../../../pages/main/study/config/study-country/manage-study-country/manage-study-country.component';
import { ServiceTypeComponent } from '../../../pages/main/study/config/service-type/service-type.component';
import { ManageServiceTypeComponent } from 'src/app/pages/main/study/config/service-type/manage-service-type/manage-service-type.component';
import { PhaseComponent } from '../../../pages/main/study/config/phase/phase.component';
import { ManagePhaseComponent } from '../../../pages/main/study/config/phase/manage-phase/manage-phase.component';
import { VisitComponent } from '../../../pages/main/study/config/visit/visit.component';
import { ManageVisitComponent } from '../../../pages/main/study/config/visit/manage-visit/manage-visit.component';
import { ManageSiteComponent } from '../../../pages/main/study/sites/manage-site/manage-site.component';
import { ManageSitePhaseComponent } from '../../../pages/main/study/sites/manage-site-phase/manage-site-phase.component';
import { EthnicityComponent } from '../../../pages/main/study/config/ethnicity/ethnicity.component';
import { ManageEthnicityComponent } from '../../../pages/main/study/config/ethnicity/manage-ethnicity/manage-ethnicity.component';
import { RaceComponent } from '../../../pages/main/study/config/race/race.component';
import { ManageRaceComponent } from '../../../pages/main/study/config/race/manage-race/manage-race.component';
import { ManageSubjectComponent } from '../../../pages/main/study/subjects/manage-subject/manage-subject.component';
import { ManageSubjectPhaseComponent } from '../../../pages/main/study/subjects/manage-subject-phase/manage-subject-phase.component';

@NgModule({
  declarations: [
    SubjectsComponent,
    SitesComponent,
    ConfigComponent,
    TherapeuticAreaComponent,
    ManageTherapeuticAreaComponent,
    TrialClassificationComponent,
    ManageTrialClassificationComponent,
    StudyCountryComponent,
    ManageStudyCountryComponent,
    ServiceTypeComponent,
    ManageServiceTypeComponent,
    PhaseComponent,
    ManagePhaseComponent,
    VisitComponent,
    ManageVisitComponent,
    ManageSiteComponent,
    ManageSitePhaseComponent,
    EthnicityComponent,
    ManageEthnicityComponent,
    RaceComponent,
    ManageRaceComponent,
    ManageSubjectComponent,
    ManageSubjectPhaseComponent
  ],
  imports: [
    CommonModule,
    StudyRoutingModule,
    ReusableModule
  ]
})
export class StudyModule { }
