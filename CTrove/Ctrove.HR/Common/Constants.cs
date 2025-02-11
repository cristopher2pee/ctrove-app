using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Common
{
    public static class Constants
    {
        public const string ApiKeyHeaderName = "XApiKey";
    }

    public static class HR_API_URL
    {
        public const string GENERATE_TOKEN = "api/Auth/generate-token";
        public const string GET_COUNTRY_LIST = "api/Country";


        //Contributor
        public const string URL_CONTRIBUTOR = "api/Contributor";
        public const string URL_SEARCH_CONTRIBUTOR = "api/Contributor/search-contributor";
        public const string URL_CONTRIBUTOR_PAGE_LIST = "api/Contributor/page-list";
        public const string GET_CONTRIBUTOR_BY_ID = "api/Contributor/id";
        public const string URL_GENERATE_TOKEN = "api/Auth/generate-token";

        public const string URL_ORGANIZATION = "api/Organization";
        public const string URL_CONTRIBUTOR_STUDY = "api/ContributorStudy";
        public const string URL_CONTACT_TYPE = "api/ContactType";
    }

    public static class MessageShow
    {
        public const string ERROR_SAVING = "Error in Saving Entity.";
        public const string ERROR_UPDATING = "Error in Updaring Entity.";
        public const string ERROR_NOTFOUND = "Entity Not Found.";
        public const string ERROR_DELETING = "Error in Deleting Entity.";
        public const string ERROR_DEACTIVATE = "Error in Deactivating Entity.";
    }
}
