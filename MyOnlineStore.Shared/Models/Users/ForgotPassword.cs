using System;

namespace MyOnlineStore.Entities.Models.Users
{
    public class ForgotPassword
    {
        public string UserEmail { get; set; }
        public string Code { get; set; }
        public string VerifyCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DateCreate { get; set; }

        public ForgotPassword()
        {
            UserEmail = string.Empty;
            Code = string.Empty;
            VerifyCode = string.Empty;
            ExpirationDate = new DateTime();
            DateCreate = new DateTime();
        }
    }
}