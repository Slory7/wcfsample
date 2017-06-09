﻿using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;

namespace Repository.Pattern
{
    public class ReadOnlyUnitOfWork : IReadOnlyUnitOfWork
    {
        private bool _disposed;

        private IReadOnlyDataContext _database;

        public Guid _sGUID = Guid.NewGuid();
        public ReadOnlyUnitOfWork(IReadOnlyDataContext database)
        {
            _database = database;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    var petaDatabase = _database as PetaDataContext;
                    if (petaDatabase != null && petaDatabase.Connection != null && petaDatabase.Connection.State == ConnectionState.Open)
                    {
                        petaDatabase.Connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (_database != null)
                {
                    _database.Dispose();
                    _database = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion

        public IReadOnlyDataContext Database
        {
            get { return _database; }
        }

        /// <summary>
        ///     Starts or continues a transaction.
        /// </summary>
        /// <returns>An TransactionObject reference that must be Completed or disposed</returns>
        /// <remarks>
        ///     This method makes management of calls to Begin/End/CompleteTransaction easier.
        ///     The usage pattern for this should be:
        ///     using (var tx = _UnitOfWork.GetTransaction())
        ///     {
        ///     // Do stuff
        ///     _bizManager.Update(...);
        ///     // Mark the transaction as complete
        ///     tx.Complete();
        ///     }
        ///     Transactions can be nested but they must all be completed otherwise the entire
        ///     transaction is aborted.
        /// </remarks>
        public ITransactionObject GetTransactionObject()
        {
            return new TransactionObject(_database);
        }
    }
}
