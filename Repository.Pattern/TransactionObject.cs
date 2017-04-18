using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;
using Repository.Pattern.NIS;

namespace Repository.Pattern
{
    /// <summary>
    ///     Transaction object helps maintain transaction
    /// </summary>
    public class TransactionObject : ITransactionObject
    {
        private IDataContext _db;
        private IReadOnlyDataContext _db2;

        public TransactionObject(IDataContext db)
        {
            _db = db;
            _db.BeginTransaction();
        }
        public TransactionObject(IReadOnlyDataContext db)
        {
            _db2 = db;
            _db2.BeginTransaction();
        }

        public void Complete()
        {
            if (_db != null)
            {
                _db.CompleteTransaction();
                _db = null;
            }
            if (_db2 != null)
            {
                _db2.CompleteTransaction();
                _db2 = null;
            }
        }

        public void Dispose()
        {
            if (_db != null)
                _db.AbortTransaction();
            if (_db2 != null)
                _db2.AbortTransaction();
        }
    }
}
