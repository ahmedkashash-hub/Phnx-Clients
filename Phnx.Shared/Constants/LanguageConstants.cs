using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Shared.Constants
{

    public static class LanguageConstants
    {
        #region Visit Messages
        public const string VISIT_NOT_FOUND = nameof(VISIT_NOT_FOUND);
        public const string Visit_ID_REQUIRED = nameof(Visit_ID_REQUIRED);
        public const string VISIT_TIME_REQUIRED = nameof(VISIT_TIME_REQUIRED);
            #endregion



        #region Client Messages

        public const string CLIENT_NOT_FOUND = nameof(CLIENT_NOT_FOUND);
        public const string CLIENT_ID_REQUIRED = nameof(CLIENT_ID_REQUIRED);
        public const string CLIENT_NAME_REQUIRED = nameof(CLIENT_NAME_REQUIRED);
        public const string CLIENT_COMPANYNAME_REQUIRED = nameof(CLIENT_COMPANYNAME_REQUIRED);
        public const string CLIENT_EXPIRY_DATE_REQUIRED = nameof(CLIENT_EXPIRY_DATE_REQUIRED);
        #endregion

        #region Contact Messages
        public const string CONTACT_NOT_FOUND = nameof(CONTACT_NOT_FOUND);
        public const string CONTACT_ID_REQUIRED = nameof(CONTACT_ID_REQUIRED);
        public const string CONTACT_CLIENT_ID_REQUIRED = nameof(CONTACT_CLIENT_ID_REQUIRED);
        public const string CONTACT_FIRST_NAME_REQUIRED = nameof(CONTACT_FIRST_NAME_REQUIRED);
        public const string CONTACT_LAST_NAME_REQUIRED = nameof(CONTACT_LAST_NAME_REQUIRED);
        public const string CONTACT_EMAIL_REQUIRED = nameof(CONTACT_EMAIL_REQUIRED);
        public const string CONTACT_EMAIL_INVALID = nameof(CONTACT_EMAIL_INVALID);
        public const string CONTACT_PHONE_REQUIRED = nameof(CONTACT_PHONE_REQUIRED);
        public const string CONTACT_TITLE_REQUIRED = nameof(CONTACT_TITLE_REQUIRED);
        #endregion

        #region Activity Messages
        public const string ACTIVITY_NOT_FOUND = nameof(ACTIVITY_NOT_FOUND);
        public const string ACTIVITY_ID_REQUIRED = nameof(ACTIVITY_ID_REQUIRED);
        public const string ACTIVITY_SUBJECT_REQUIRED = nameof(ACTIVITY_SUBJECT_REQUIRED);
        public const string ACTIVITY_OCCURRED_AT_REQUIRED = nameof(ACTIVITY_OCCURRED_AT_REQUIRED);
        #endregion

        #region Task Messages
        public const string TASK_NOT_FOUND = nameof(TASK_NOT_FOUND);
        public const string TASK_ID_REQUIRED = nameof(TASK_ID_REQUIRED);
        public const string TASK_TITLE_REQUIRED = nameof(TASK_TITLE_REQUIRED);
        #endregion

        #region Invoice Messages
        public const string INVOICE_NOT_FOUND = nameof(INVOICE_NOT_FOUND);
        public const string INVOICE_ID_REQUIRED = nameof(INVOICE_ID_REQUIRED);
        public const string INVOICE_CLIENT_ID_REQUIRED = nameof(INVOICE_CLIENT_ID_REQUIRED);
        public const string INVOICE_ISSUE_DATE_REQUIRED = nameof(INVOICE_ISSUE_DATE_REQUIRED);
        public const string INVOICE_DUE_DATE_REQUIRED = nameof(INVOICE_DUE_DATE_REQUIRED);
        public const string INVOICE_DUE_DATE_INVALID = nameof(INVOICE_DUE_DATE_INVALID);
        public const string INVOICE_SUBTOTAL_INVALID = nameof(INVOICE_SUBTOTAL_INVALID);
        public const string INVOICE_TAX_INVALID = nameof(INVOICE_TAX_INVALID);
        public const string INVOICE_TOTAL_INVALID = nameof(INVOICE_TOTAL_INVALID);
        public const string INVOICE_CURRENCY_REQUIRED = nameof(INVOICE_CURRENCY_REQUIRED);
        #endregion

        #region Lead Messages
        public const string LEAD_NOT_FOUND = nameof(LEAD_NOT_FOUND);
        public const string LEAD_ID_REQUIRED = nameof(LEAD_ID_REQUIRED);
        public const string LEAD_COMPANY_NAME_REQUIRED = nameof(LEAD_COMPANY_NAME_REQUIRED);
        public const string LEAD_CONTACT_NAME_REQUIRED = nameof(LEAD_CONTACT_NAME_REQUIRED);
        public const string LEAD_EMAIL_REQUIRED = nameof(LEAD_EMAIL_REQUIRED);
        public const string LEAD_EMAIL_INVALID = nameof(LEAD_EMAIL_INVALID);
        public const string LEAD_PHONE_REQUIRED = nameof(LEAD_PHONE_REQUIRED);
        public const string LEAD_SOURCE_REQUIRED = nameof(LEAD_SOURCE_REQUIRED);
        public const string LEAD_TITLE_REQUIRED = nameof(LEAD_TITLE_REQUIRED);
        #endregion

        #region Opportunity Messages
        public const string OPPORTUNITY_NOT_FOUND = nameof(OPPORTUNITY_NOT_FOUND);
        public const string OPPORTUNITY_ID_REQUIRED = nameof(OPPORTUNITY_ID_REQUIRED);
        public const string OPPORTUNITY_NAME_REQUIRED = nameof(OPPORTUNITY_NAME_REQUIRED);
        public const string OPPORTUNITY_VALUE_INVALID = nameof(OPPORTUNITY_VALUE_INVALID);
        public const string OPPORTUNITY_PROBABILITY_INVALID = nameof(OPPORTUNITY_PROBABILITY_INVALID);
        #endregion

        #region Service Messages
        public const string SERVICE_NOT_FOUND = nameof(SERVICE_NOT_FOUND);
        public const string SERVICE_ID_REQUIRED = nameof(SERVICE_ID_REQUIRED);
        public const string SERVICE_NAME_REQUIRED = nameof(SERVICE_NAME_REQUIRED);
        #endregion

        #region Project Messages

        public const string PROJECT_NOT_FOUND = nameof(PROJECT_NOT_FOUND);
        public const string PROJECT_ID_REQUIRED = nameof(PROJECT_ID_REQUIRED);
        public const string PROJECT_NAME_REQUIRED = nameof(PROJECT_NAME_REQUIRED);
        public const string PROJECT_DESCRIPTION_REQUIRED = nameof(PROJECT_DESCRIPTION_REQUIRED);
        #endregion



        #region Payment Messages

        public const string PAYMENT_NOT_FOUND = nameof(PAYMENT_NOT_FOUND);
        public const string PAYMENT_ID_REQUIRED = nameof(PAYMENT_ID_REQUIRED);
        public const string PAYMENT_TYPE_REQUIRED = nameof(PAYMENT_TYPE_REQUIRED);
        public const string PAYMENT_AMOUNT_REQUIRED = nameof(PAYMENT_AMOUNT_REQUIRED);
        public const string PAYMENT_DUE_DATE_REQUIRED = nameof(PAYMENT_DUE_DATE_REQUIRED);
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
        public const string USER_ADMIN_UPDATE_FORBIDDEN = nameof(USER_ADMIN_UPDATE_FORBIDDEN);
        public const string USER_ADMIN_DELETE_FORBIDDEN = nameof(USER_ADMIN_DELETE_FORBIDDEN);
        #endregion
    }

}
