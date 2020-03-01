using MyOnlineStore.Application.Data.Models.Entries;
using MyOnlineStore.Application.Common.Interfaces.Entries;
using System;

namespace MyOnlineStore.Testing.MockData.Entries
{
    public class MockRegisterEntry : IRegisterEntry
    {
        public MockRegisterEntry()
        {
            FullName = "Ricardo Torres";
            Email = "torres@gmail.com";
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
