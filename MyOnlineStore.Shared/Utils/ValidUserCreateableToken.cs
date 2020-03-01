using System;

namespace MyOnlineStore.Models.Utils
{
    public class ValidUserCreateableToken
    {
        public bool ValidCredentials { get; set; }
        public string TypeOfUser { get; set; } = string.Empty;

        public ValidUserCreateableToken()
        {
        }

        public ValidUserCreateableToken(Tuple<bool, string> tuple)
        {
            ValidCredentials = tuple.Item1;
            TypeOfUser = tuple.Item2;
        }

        public ValidUserCreateableToken(bool validToCreate, string typeOfUser)
        {
            ValidCredentials = validToCreate;
            TypeOfUser = typeOfUser;
        }
    }
}