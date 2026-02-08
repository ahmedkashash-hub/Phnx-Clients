using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Shared.Constants
{

    public static class LanguageConstants
    {
      
        #region Project Messages

        public const string PROJECT_NOT_FOUND = nameof(PROJECT_NOT_FOUND);
        public const string PROJECT_ID_REQUIRED = nameof(PROJECT_ID_REQUIRED);
        public const string PROJECT_NAME_REQUIRED = nameof(PROJECT_NAME_REQUIRED);
        public const string PROJECT_DESCRIPTION_REQUIRED = nameof(PROJECT_DESCRIPTION_REQUIRED);
        #endregion

        #region Client Messages

        public const string CLIENT_NOT_FOUND = nameof(CLIENT_NOT_FOUND);
        public const string CLIENT_ID_REQUIRED = nameof(CLIENT_ID_REQUIRED);
        public const string CLIENT_NAME_REQUIRED = nameof(CLIENT_NAME_REQUIRED);
        public const string CLIENT_COMPANTNAME_REQUIRED = nameof(CLIENT_COMPANTNAME_REQUIRED);
        #endregion

        #region Payment Messages

        public const string PAYMENT_NOT_FOUND = nameof(PAYMENT_NOT_FOUND);
        public const string PAYMENT_TYPE_REQUIRED = nameof(PAYMENT_TYPE_REQUIRED);
        public const string PAYMENT_AMOUNT_REQUIRED = nameof(PAYMENT_AMOUNT_REQUIRED);
        public const string PAYMENT_METHOD_REQUIRED = nameof(PAYMENT_METHOD_REQUIRED);
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
