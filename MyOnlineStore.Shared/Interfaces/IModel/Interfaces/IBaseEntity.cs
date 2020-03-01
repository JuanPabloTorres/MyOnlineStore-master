using System;

namespace MyOnlineStore.Entities.Models.Interfaces
{
    public interface IBaseEntity
    {
        //string CreatedBy { get; }
        //string UpdatedBy { get; }
        //DateTime CreatedDateTime { get; }
        //DateTime UpdateDateTime { get; }

        #region Properties

        Guid Id { get; }
        bool IsAlive { get; }

        #endregion Properties
    }
}