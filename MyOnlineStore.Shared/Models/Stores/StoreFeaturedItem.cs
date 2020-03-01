using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Stores
{
    [Table("FeaturedItems")]
    public class StoreFeaturedItem /*: IBaseEntity*/
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[] Image { get; set; }
        public Guid MyStoreId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int DisplayPosition { get; set; }
        //public string CreatedBy { get;  set; } = string.Empty;

        //public string UpdatedBy { get;  set; } = string.Empty;

        //public DateTime CreatedDateTime { get; set; }

        //public DateTime UpdateDateTime { get; set; }

        //public bool IsAlive { get; set; }
        public StoreFeaturedItem()
        {
            Image = new byte[1];
        }
    }
}