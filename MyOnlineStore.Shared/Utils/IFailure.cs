using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils
{
    public interface IFailure
    {
        List<string> ErrorsMessages { get; }
    }
}