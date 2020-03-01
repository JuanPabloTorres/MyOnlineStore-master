using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyOnlineStore.Shared.Models.Users
{
    public class UserConnection
    {
        [Key]
        public string ConnectionID { get; set; }
      
        public bool Connected { get; set; }

        public Guid UserId { get; set; }
    }
}
