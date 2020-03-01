using MyOnlineStore.Entities.Models.Users;
using System;

namespace MyOnlineStore.Application.Common.Utilities
{
    public class UserTuple : Tuple<User, string, string>
    {
        #region Fields

        private dynamic? user;

        private string email = string.Empty;

        private string pass = string.Empty;

        #endregion Fields

        #region Constructors

        public UserTuple(dynamic CurrentUser, string email, string pass)
            : base((User)CurrentUser, email, pass)
        {
            User = Item1;
            Email = Item2;
            Password = Item3;
        }

        #endregion Constructors

        #region Properties

        public dynamic? User
        {
            get { return user; }
            set { user = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; }
        }

        #endregion Properties

        #region Methods

        public static bool HasValues(UserTuple userTuple)
        {
            bool validated = false;

            if(userTuple != null)
            {
                if(string.IsNullOrEmpty(userTuple.Email) || string.IsNullOrEmpty(userTuple.Password) || userTuple.User == null)
                {
                    validated = true;
                }
            }

            return validated;
        }

        #endregion Methods
    }
}