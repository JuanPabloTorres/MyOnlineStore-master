namespace MyOnlineStore.Shared.Utils
{
    public class ApiResponse<TData> : IResponse<ApiFailure, ApiSuccess<TData>> 
        where TData : class, new()
    {
        #region Constructors

        public ApiResponse()
        {
            Failure = new ApiFailure();
            Success = new ApiSuccess<TData>();
        }

        #endregion Constructors

        #region Properties

        public ApiFailure Failure { get; set; }
        public ApiSuccess<TData> Success { get; set; }
        public bool IsSuccess { get; set; }

        public void DisplayAllMessages()
        {
            if(IsSuccess)
            {
                foreach(string message in Success.SuccessMessages) { 
                }
            }
        }

        #endregion Properties
    }
}