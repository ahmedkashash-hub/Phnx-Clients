using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Shared.Constants
{

    public static class LanguageConstants
    {

        public const string EMPTY_PASSWORD = nameof(EMPTY_PASSWORD);
        public const string EMPTY_EMAIL = nameof(EMPTY_EMAIL);
        public const string INVALID_CREDENTIALS = nameof(INVALID_CREDENTIALS);
        public const string TOKEN_NOT_PROVIDED = nameof(TOKEN_NOT_PROVIDED);
        public const string REFRESH_TOKEN_NOT_PROVIDED = nameof(REFRESH_TOKEN_NOT_PROVIDED);
        public const string EXPIRED_SESSION = nameof(EXPIRED_SESSION);
        #endregion

        #region Advertisement Messages

        public const string IMAGE_REQUIRED = nameof(IMAGE_REQUIRED);
        public const string ADVERTISEMENT_NOT_FOUND = nameof(ADVERTISEMENT_NOT_FOUND);
        public const string ADVERTISEMENT_ID_REQUIRED = nameof(ADVERTISEMENT_ID_REQUIRED);
        #endregion

        #region Job Messages

        public const string JOB_NOT_FOUND = nameof(JOB_NOT_FOUND);
        public const string JOB_ID_REQUIRED = nameof(JOB_ID_REQUIRED);
        public const string JOB_TITLE_REQUIRED = nameof(JOB_TITLE_REQUIRED);
        public const string JOB_DESCRIPTION_REQUIRED = nameof(JOB_DESCRIPTION_REQUIRED);
        #endregion

        #region Department Messages

        public const string DEPARTMENT_NOT_FOUND = nameof(DEPARTMENT_NOT_FOUND);
        public const string DEPARTMENT_ID_REQUIRED = nameof(DEPARTMENT_ID_REQUIRED);
        public const string DEPARTMENT_NAME_REQUIRED = nameof(DEPARTMENT_NAME_REQUIRED);
        #endregion



        #region User Messages

        public const string USER_NOT_FOUND = nameof(USER_NOT_FOUND);
        public const string USER_ID_REQUIRED = nameof(USER_ID_REQUIRED);
        public const string USER_NAME_REQUIRED = nameof(USER_NAME_REQUIRED);
        public const string USER_EMAIL_REQUIRED = nameof(USER_EMAIL_REQUIRED);
        public const string USER_EMAIL_INVALID = nameof(USER_EMAIL_INVALID);
        public const string USER_PASSWORD_REQUIRED = nameof(USER_PASSWORD_REQUIRED);
        public const string USER_EMAIL_ALREADY_EXISTS = nameof(USER_EMAIL_ALREADY_EXISTS);
        public const string USER_PERMISSIONS_INVALID = nameof(USER_PERMISSIONS_INVALID);
        #endregion
    }
}
