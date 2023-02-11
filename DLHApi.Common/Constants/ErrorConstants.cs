namespace DLHApi.Common.Constants
{
    public static  class ErrorConstants
    {
        public const string UnAuthorized = "Request denied. Unauthorized access.";

        public const string ValidationError = "Request responded with one or more validation errors.";

        public const string UnhandledEx = "Unable to process the request. Unhandled Exception occurred.";

        public const string ObjNotSet = "Object: {0} not set.";

        public const string NoData = "Request returned No Data.";

        public const string IncorrectMvID = "Input MvId:{0} is invalid.";

        public const string IncorrectUsrnameOrPsswrd = "Username or Password is incorrect."; 
    }
}
