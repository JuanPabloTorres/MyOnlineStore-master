namespace MyOnlineStore.Application.Data.Models.Entries
{
    public class LoginEntry
    {
        public LoginEntry()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
