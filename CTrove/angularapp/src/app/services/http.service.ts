import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

// Base
export const BASE_URL: string = environment.apiUrl;
export const API_URL: string = '/api';
export const APP_URL: string = BASE_URL + API_URL;

// Access
export const ACCESS_URL = APP_URL + '/Access';
export const ACCESS_LIST_URL = ACCESS_URL + '/list-page';
export const ACCESS_IS_EXISTING_URL = ACCESS_URL + '/already-exist';

// Accounts
export const ACCOUNTS_URL = APP_URL + '/Accounts';
export const ACCOUNTS_PROFILE_URL = ACCOUNTS_URL + '/profile';
export const ACCOUNTS_ONBOARDING = ACCOUNTS_URL + '/onboarding';
export const ACCOUNTS_IS_ONBOARDING = ACCOUNTS_URL + '/isOnBoarding';
export const INVITE_USER_URL = ACCOUNTS_URL + '/invite';
export const INVITE_EMAIL_URL = ACCOUNTS_URL + '/invite-email';

// Audit
export const AUDIT_URL = APP_URL + '/Audit';
export const AUDIT_LIST_URL = AUDIT_URL + '/id/list-by-record-id';

// Ethnicity
export const ETHNICITY_URL = APP_URL + '/Ethnicity';

// Phase
export const PHASE_URL = APP_URL + '/Phase';

// Race
export const RACE_URL = APP_URL + '/Race';

// Roles
export const ROLES_URL = APP_URL + '/Roles';
export const ROLES_RESOURCE_URL = ROLES_URL + '/list-resources';
export const ROLES_PAGES_URL = ROLES_URL + '/roles-pages';

// Service Type
export const SERVICE_TYPE_URL = APP_URL + '/ServiceTypes';

// Study Country
export const STUDY_COUNTRY_URL = APP_URL + '/StudyCountry';

// Subject
export const SUBJECT_URL = APP_URL + '/Subject';
export const SUBJECT_PHASE_URL = SUBJECT_URL + '/subject-phases';

// Sites
export const STUDY_SITES_URL = APP_URL + '/Sites';
export const STUDY_SITE_PHASE_SURL = STUDY_SITES_URL + '/site-phases';

// Study
export const STUDY_URL = APP_URL + '/Study';

// Therapeutic Area
export const THERAPEUTIC_AREA_URL = APP_URL + '/TherapeuticArea';

// Trial Classification
export const TRIAL_CLASSIFICATION_URL = APP_URL + '/Classification';

// Visit
export const VISIT_URL = APP_URL + '/Visits';

// User
export const USER_URL = APP_URL + '/User';
export const USER_ACCESS_RIGHTS_URL = USER_URL + '/id/access-rights';
export const USER_EMAIL_EXIST_URL = USER_URL + '/user-email-exist';

// GOOGLE
export const GOOGLE_MAP_URL =
  'https://maps.googleapis.com/maps/api/geocode/json';

//CONTRIBUTOR URL
//Contributor
export const CONTRIBUTOR_URL = APP_URL + '/Contributor';
export const CONTRIBUTOR_NAME_EXIST_URL = CONTRIBUTOR_URL + '/name-exist';
export const CONTRIBUTOR_EMAIL_EXIST_URL = CONTRIBUTOR_URL + '/email-exist';
export const CONTRIBUTOR_SEARCH_URL = CONTRIBUTOR_URL + '/search-contributor';

//Organization
export const ORGANIZATION_URL = APP_URL + '/Organization';
export const ORGANIZATION_EXIST_URL = APP_URL + '/Organization/company-exist';

//Country
export const COUNTRY_URL = APP_URL + '/Country';

//Contact Type
export const CONTACT_TYPE_URL = APP_URL + '/ContactType';

//Vendor Type
export const VENDOR_TYPE_URL = APP_URL + '/VendorType';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(private httpClient: HttpClient) {}

  post = <T>(url: string, dto: any) => this.httpClient.post<T>(url, dto);

  get = <T>(url: string, params: any = null) =>
    params
      ? this.httpClient.get<T>(url, params as object)
      : this.httpClient.get<T>(url);

  put = <T>(url: string, dto: any) => this.httpClient.put<T>(url, dto);

  delete = <T>(url: string, dto: any) =>
    this.httpClient.request<T>('DELETE', url, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: dto,
    });
}
