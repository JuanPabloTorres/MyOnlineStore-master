using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils
{
    public class ApiFailure : IFailure
    {
        #region Constructors

        public ApiFailure()
        {
            ErrorsMessages = new List<string>();
        }

        #endregion Constructors

        #region Properties

        public List<string> ErrorsMessages { get; set; }

        #endregion Properties
    }
}