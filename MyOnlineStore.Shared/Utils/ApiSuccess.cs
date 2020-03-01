using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils
{
    public class ApiSuccess<TData> : ISuccess where TData : class, new()
    {
        #region Constructors

        public ApiSuccess()
        {
            SuccessMessages = new List<string>();
            Data = new TData();
        }

        #endregion Constructors

        #region Properties

        public List<string> SuccessMessages { get; set; }
        public TData Data { get; set; }

        #endregion Properties
    }
}