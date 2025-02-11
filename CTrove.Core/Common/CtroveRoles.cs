using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CTrove.Core.Common
{
    public class CtroveRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";

        //UNBLIDED
        public const string CLCIS_UBLD = "CLCIS-UBLD";// CL - CIS
        public const string CLCRA_UBLD = "CLCRA-UBLD";//  CL - CRA
        public const string CLCTA_UBLD = "CLCTA-UBLD"; //CL - CTA
        public const string CLDM_UBLD = "CLDM-UBLD";//    CL - Data Manager
        public const string CLPM_UBLD = "CLPM-UBLD";// CL - Project Manager
        public const string CLQA_UBLD = "CLQA-UBLD";// CL - Quality Assurance
        public const string CLREG_UBLD = "CLREG-UBLD";// CL - Regulatory

        public const string SPCRA_UBLD = "SPCRA-UBLD";// Sponsor - CRA
        public const string SPCTA_UBLD = "SPCTA-UBLD";// Sponsor - CTA
        public const string SPDM_UBLD = "SPDM-UBLD";// Sponsor - Data Manager
        public const string SPMD_UBLD = "SPMD-UBLD"; //Sponsor - Medical
        public const string SPPM_UBLD = "SPPM-UBLD";// Sponsor - Project Manager
        public const string SPQA_UBLD = "SPQA-UBLD";// Sponsor - Quality Assurance
        public const string SPREG_UBLD = "SPREG-UBLD";// Sponsor - Regulatory

        public const string STDM_UBLD = "STDM-UBLD";// Site - Data Manager
        public const string STPI_UBLD = "STPI-UBLD";// Site - Principal Investigator
        public const string STPM_UBLD = "STPM-UBLD";// Site - Project Manager
        public const string STSI_UBLD = "STSI-UBLD"; //Site - Sub Investigator

        public const string STSTF_UBLD = "STSTF-UBLD";// Site - Staff
        public const string STCOR_UBLD = "STCOR-UBLD";// Site - Coordinator
        public const string STSPLD_UBLD = "STSPLD-UBLD";// Site - Support Lead
        public const string STSPAS_UBLD = "STSPAS-UBLD"; //Site - Support Lead Associate

        //BLINDED
        public const string CLCIS_BLD = "CLCIS-BLD";// CL - CIS
        public const string CLCRA_BLD = "CLCRA-BLD";//  CL - CRA
        public const string CLCTA_BLD = "CLCTA-BLD"; //CL - CTA
        public const string CLDM_BLD = "CLDM-BLD";//    CL - Data Manager
        public const string CLPM_BLD = "CLPM-BLD";// CL - Project Manager
        public const string CLQA_BLD = "CLQA-BLD";// CL - Quality Assurance
        public const string CLREG_BLD = "CLREG-BLD";// CL - Regulatory

        public const string SPCRA_BLD = "SPCRA-BLD";// Sponsor - CRA
        public const string SPCTA_BLD = "SPCTA-BLD";// Sponsor - CTA
        public const string SPDM_BLD = "SPDM-BLD";// Sponsor - Data Manager
        public const string SPMD_BLD = "SPMD-BLD"; //Sponsor - Medical
        public const string SPPM_BLD = "SPPM-BLD";// Sponsor - Project Manager
        public const string SPQA_BLD = "SPQA-BLD";// Sponsor - Quality Assurance
        public const string SPREG_BLD = "SPREG-BLD";// Sponsor - Regulatory

        public const string STDM_BLD = "STDM-BLD";// Site - Data Manager
        public const string STPI_BLD = "STPI-BLD";// Site - Principal Investigator
        public const string STPM_BLD = "STPM-BLD";// Site - Project Manager
        public const string STSI_BLD = "STSI-BLD"; //Site - Sub Investigator

        public const string STSTF_BLD = "STSTF-BLD";// Site - Staff
        public const string STCOR_BLD = "STCOR-BLD";// Site - Coordinator
        public const string STSPLD_BLD = "STSPLD-BLD";// Site - Support Lead
        public const string STSPAS_BLD = "STSPAS-BLD"; //Site - Support Lead Associate
    }
}
