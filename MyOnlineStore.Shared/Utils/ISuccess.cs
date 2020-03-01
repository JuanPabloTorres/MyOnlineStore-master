using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils
{
    public interface ISuccess
    {
        List<string> SuccessMessages { get; set; }
    }
}