using MyOnlineStore.Application.Common.Interfaces.Entries;
using System;

namespace MyOnlineStore.Application.Data.Models.Entries
{
    public class RegisterEntry : IRegisterEntry
    {
        public RegisterEntry()
        {
            FullName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PhoneNumber = string.Empty;
            BirthDate = new DateTime();
            CardInformation = new UserCardEntry();
        }
      

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserCardEntry CardInformation { get; set; }
        public string PhoneNumber { get; set; }
    }

 
}
