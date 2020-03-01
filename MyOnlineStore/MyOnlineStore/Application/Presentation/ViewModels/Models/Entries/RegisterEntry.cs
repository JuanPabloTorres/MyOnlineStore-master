
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.ViewModels.Models.Entries
{
    public class RegisterEntry
    {
        public RegisterEntry()
        {

        }
        //public RegisterEntry(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public ValidatableObject FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public string UserRole { get; set; }      

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public UserCardEntry CardInformation { get; set; }

        public string PhoneNumber { get; set; }

        //public bool AdminRegistration()
        //{
        //    return UserRole == _configuration.GetSection("Roles:Admin").Value ? true : false;
        //}

    }

 
}
