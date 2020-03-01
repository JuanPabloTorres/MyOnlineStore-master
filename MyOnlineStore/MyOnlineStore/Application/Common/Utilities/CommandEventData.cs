using System;

namespace MyOnlineStore.Application.Common.Utilities
{
    public class CommandEventData
    {
        public object? Sender { get; set; }
        public object? Parameter { get; set; }
        public object? Args { get; set; }
    }
}
