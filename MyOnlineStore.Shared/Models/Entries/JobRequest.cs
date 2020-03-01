using MyOnlineStore.Entities.Models.Interfaces;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyOnlineStore.Shared.Models.Entries
{
    [Table("JobRequest")]
   public class JobRequest : IBaseEntity
    {
        public Guid UserReciverId { get; set; }

        public Guid UserSenderId { get; set; }

        public Guid StoreId { get; set; }

        public string SenderFullName { get; set; } = string.Empty;

        public string SenderStoreName { get; set; } = string.Empty;

        public RequestFlag RequestAnwser { get; set; }

        public DateTime ExpDate { get; set; }

        public Guid Id { get; set; }

        public bool IsAlive { get; set; }
    }

    public enum RequestFlag
    {
        None,
        Approved,
        Rejected,
       
    };
}
