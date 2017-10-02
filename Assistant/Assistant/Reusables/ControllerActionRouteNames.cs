namespace Assistant.Reusables
{
    public struct ControllerActionRouteNames
    {
        #region Sub Structs
        public struct Home
        {
            public const string DASHBOARD = "Dashboard";
        }

        public struct Users
        {
            public const string USERS = "Users";
            public const string USERS_ADD = "UsersAdd";
            public const string USERS_UPDATE = "UsersUpdate";
            public const string USERS_DELETE = "UsersDelete";

            public const string USER_EXPENSES = "UserExpenses";
            public const string USER_EXPENSES_FILTERED = "UserExpensesFiltered";
            public const string USER_EXPENSES_EXPORT_TO_EXCEL = "UserExpensesExportToExcel";
            public const string USER_EXPENSES_CREATE = "UserExpensesCreate";
            public const string USER_EXPENSES_UPDATE = "UserExpensesUpdate";
            public const string USER_EXPENSES_DELETE = "UserExpensesDelete";

            public const string USER_MEETINGS = "UserMeetings";
            public const string USER_MEETINGS_FILTERED = "UserMeetingsFiltered";
            public const string USER_MEETINGS_EXPORT_TO_EXCEL = "UserMeetingsExportToExcel";
            public const string USER_MEETINGS_CREATE = "UserMeetingsCreate";
            public const string USER_MEETINGS_UPDATE = "UserMeetingsUpdate";
            public const string USER_MEETINGS_DELETE = "UserMeetingsDelete";

        }

        public struct Shared
        {
            public const string NOT_FOUND = "NotFound";
        }
        #endregion
    }
}