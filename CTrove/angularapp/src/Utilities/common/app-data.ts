import { SIDE_MENU } from 'src/app/models/common/main';
import {
  ACCESS_PAGE,
  CONFIG_PAGE,
  CONTRIBUTORS_PAGE,
  DASHBOARD_PAGE,
  ORGANIZATION_PAGE,
  ROLES_PAGE,
  SCHEDULED,
  SITES_PAGE,
  SUBJECTS_PAGE,
  UNSCHEDULED,
  USER_PAGE,
} from './app-strings';
import { RolesPage } from 'src/app/models/dto/role';
import { Guid } from 'guid-typescript';

export const MAIN_MENU: SIDE_MENU[] = [
  {
    name: 'Home',
    icon: 'home',
    path: './home',
    isSelected: false,
    subMenu: [
      {
        name: DASHBOARD_PAGE,
        icon: null,
        path: './home/dashboard',
        subMenu: null,
        isSelected: false,
      },
    ],
  },
  {
    name: 'Study',
    icon: 'read',
    path: './study',
    isSelected: false,
    subMenu: [
      {
        name: SUBJECTS_PAGE,
        icon: null,
        path: './study/subjects',
        subMenu: null,
        isSelected: false,
      },
      {
        name: SITES_PAGE,
        icon: null,
        path: './study/sites',
        subMenu: null,
        isSelected: false,
      },
      {
        name: CONFIG_PAGE,
        icon: null,
        path: './study/config',
        subMenu: null,
        isSelected: false,
      },
    ],
  },
  {
    name: 'Admin',
    icon: 'safety-certificate',
    path: './admin',
    isSelected: false,
    subMenu: [
      {
        name: ACCESS_PAGE,
        icon: null,
        path: './admin/access',
        subMenu: null,
        isSelected: false,
      },
      {
        name: ROLES_PAGE,
        icon: null,
        path: './admin/roles',
        subMenu: null,
        isSelected: false,
      },
      {
        name: USER_PAGE,
        icon: null,
        path: './admin/user',
        subMenu: null,
        isSelected: false,
      },
      // {
      //   name: INVITE_PAGE,
      //   icon: null,
      //   path: './admin/invite',
      //   subMenu: null,
      //   isSelected: false,
      // },
    ],
  },
  {
    name: 'Contributor',
    icon: 'usergroup-add',
    path: './contributor',
    isSelected: false,
    subMenu: [
      {
        name: CONTRIBUTORS_PAGE,
        icon: null,
        path: './contributor/contributors',
        subMenu: null,
        isSelected: false,
      },
      {
        name: ORGANIZATION_PAGE,
        icon: null,
        path: './contributor/organizations',
        subMenu: null,
        isSelected: false,
      },
    ],
  },
];

export const BLINDED_OPTIONS = [
  {
    id: true,
    name: 'Blinded',
  },
  {
    id: false,
    name: 'Unblinded',
  },
];

export const RIGHTS_OPTIONS = [
  {
    id: 1,
    name: 'Read',
    isDisabled: true,
  },
  {
    id: 2,
    name: 'Write',
  },
  {
    id: 4,
    name: 'Bin',
  },
];

export const VISIT_TYPE_OPTIONS = [
  {
    id: 1,
    name: SCHEDULED,
  },
  {
    id: 2,
    name: UNSCHEDULED,
  },
];

export const STUDY_TYPE_OPTIONS = [
  {
    id: 'Open Label',
    name: 'Open Label',
  },
  {
    id: 'Blinded',
    name: 'Blinded',
  },
];

export const SITE_STATUS_OPTIONS = [
  {
    id: 0,
    name: 'Pending',
  },
  {
    id: 1,
    name: 'Active',
  },
  {
    id: 2,
    name: 'Inactive',
  },
  {
    id: 3,
    name: 'Not Selected',
  },
];

export const PAGE_OPTIONS: RolesPage[] = [
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Subject',
    status: false,
    pages: 1,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Sites',
    status: false,
    pages: 2,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Config',
    status: false,
    pages: 3,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Access',
    status: false,
    pages: 4,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Roles',
    status: false,
    pages: 5,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'User',
    status: false,
    pages: 6,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Contributors',
    status: false,
    pages: 7,
  },
  {
    id: Guid.EMPTY,
    rolesId: Guid.EMPTY,
    name: 'Organizations',
    status: false,
    pages: 8,
  }
];

export const GENDER_OPTIONS = [
  {
    id: 1,
    name: 'Male',
  },
  {
    id: 2,
    name: 'Female',
  },
  {
    id: 3,
    name: 'Others',
  },
];

export const SUBJECT_STATUS_OPTIONS = [
  {
    id: 1,
    name: 'Completed',
  },
  {
    id: 2,
    name: 'Ongoing',
  },
  {
    id: 3,
    name: 'Randomized',
  },
  {
    id: 4,
    name: 'Withdrawn',
  },
  {
    id: 5,
    name: 'Screen Failed',
  },
  {
    id: 6,
    name: 'Discontinued',
  },
]

export const PARENT_TYPE_OPTIONS = [
  {
    id: 1,
    name: 'No',
  },
  {
    id: 2,
    name: 'Linked',
  },
  {
    id: 3,
    name: 'Yes',
  }
]

export const PREFIX_TYPE_OPTIONS = [
  {
    id: 1,
    name: 'Mr',
  },
  {
    id: 2,
    name: 'Mrs',
  },
  {
    id: 3,
    name: 'Ms',
  },
  {
    id: 4,
    name: 'Miss',
  },
  {
    id: 5,
    name: 'Dr',
  },
  {
    id: 6,
    name: 'ProfDr',
  },
  {
    id: 7,
    name: 'Prof',
  },
  {
    id: 8,
    name: 'Drs',
  },
  {
    id: 9,
    name: 'Other',
  }
]

export const GRADE_TYPE_OPTIONS = [
  {
    id: 1,
    name: 'MD',
  },
  {
    id: 2,
    name: 'MDPhD',
  },
  {
    id: 3,
    name: 'PhD',
  },
  {
    id: 4,
    name: 'MSc',
  },
  {
    id: 5,
    name: 'MBA',
  },
  {
    id: 6,
    name: 'BSc',
  },
  {
    id: 7,
    name: 'Other',
  }
]
