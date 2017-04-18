using System;

namespace Repository.Pattern.Interface
{
    public interface ITransactionObject : IDisposable
    {
        /// <summary>
        ///     Completes the transaction. Not calling complete will cause the transaction to rollback on dispose.
        /// </summary>
        void Complete();
    }
}
