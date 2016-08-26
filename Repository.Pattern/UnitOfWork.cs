using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;

namespace Repository.Pattern
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private Database _database;

        public UnitOfWork()
        {
            //TODO:Or get string
            var connectionConfig = System.Configuration.ConfigurationManager.ConnectionStrings["NISOrderContext"];
            string connectionString = connectionConfig.ConnectionString;
            _database = new Database(connectionString, connectionConfig.ProviderName);
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
                    if (_database != null && _database.Connection != null && _database.Connection.State == ConnectionState.Open)
                    {
                        _database.Connection.Close();
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

        public IDatabase Database
        {
            get { return _database; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _database.IsolationLevel = isolationLevel;
            _database.BeginTransaction();
        }

        public bool Commit()
        {
            _database.CompleteTransaction();
            return true;
        }

        public void Rollback()
        {
            _database.AbortTransaction();
        }
    }
}
