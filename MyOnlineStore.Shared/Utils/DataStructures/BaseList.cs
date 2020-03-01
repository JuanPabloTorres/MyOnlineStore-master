using System.Collections.Generic;

namespace MyOnlineStore.Shared.Utils.DataStructures
{
    public abstract class BaseList<T> : List<T> where T : class
    {
        public BaseList()
        {
        }

        public BaseList(IEnumerable<T> collection) : base(collection)
        {
        }

        public BaseList(int capacity) : base(capacity)
        {
        }
    }
}