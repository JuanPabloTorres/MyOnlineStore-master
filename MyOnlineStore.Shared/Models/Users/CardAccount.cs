using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOnlineStore.Entities.Models.Users
{
    [Table("CardAccounts")]
    public class CardAccount
    {
        #region Constructors

        public CardAccount()
        {
            Number = string.Empty;
            Exp = new DateTime();
            Pin = string.Empty;
            Type = string.Empty;
            SecurityCode = string.Empty;
            CardHolderName = string.Empty;
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Exp { get; set; }
        public string Pin { get; set; }
        public string Type { get; set; }
        public bool IsVerified { get; set; }
        public string CardHolderName { get; set; }
        public string SecurityCode { get; set; }
        public Guid MyUserId { get; set; }
        public Guid? MyStoreId { get; set; }

        #endregion Properties

        #region Methods

        public bool HasAllRequierdFields()
        {
            return Number is object && Exp != null && Type is object && CardHolderName is object;
        }
        
        #endregion Methods
    }
}