using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Models.Audits
{
    [Table("Audits")]
    public class Audit
    {
        public Guid Id { get; set; }
        public string TableName { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string KeyValues { get; set; } = string.Empty;
        public string OldValues { get; set; } = string.Empty;
        public string NewValues { get; set; } = string.Empty;
        public string WhoMadeIt { get; set; } = string.Empty;
    }
}