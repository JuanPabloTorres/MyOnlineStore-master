using MyOnlineStore.Entities.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Principal;

namespace MyOnlineStore.Entities.Models.Roles
{
    public class Permission : ClaimsIdentity, IBaseEntity
    {
        #region Constructors

        public Permission()
        {
        }

        public Permission(IEnumerable<Claim> claims) : base(claims)
        {
        }

        public Permission(BinaryReader reader) : base(reader)
        {
        }

        public Permission(IIdentity identity) : base(identity)
        {
        }

        public Permission(string authenticationType) : base(authenticationType)
        {
        }

        public Permission(IEnumerable<Claim> claims, string authenticationType) : base(claims, authenticationType)
        {
        }

        public Permission(IIdentity identity, IEnumerable<Claim> claims) : base(identity, claims)
        {
        }

        public Permission(string authenticationType, string nameType, string roleType) : base(authenticationType, nameType, roleType)
        {
        }

        public Permission(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : base(claims, authenticationType, nameType, roleType)
        {
        }

        public Permission(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType) : base(identity, claims, authenticationType, nameType, roleType)
        {
        }

        protected Permission(SerializationInfo info) : base(info)
        {
        }

        protected Permission(ClaimsIdentity other) : base(other)
        {
        }

        protected Permission(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructors

        #region Properties

        public Guid Id { get; set; }

        public bool IsAlive { get; set; }

        #endregion Properties
    }
}