using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Pattern.Interface
{
    public interface IReadOnlyUnitOfWork : IDisposable
    {
        IReadOnlyDataContext Database { get; }
        void Dispose(bool disposing);
        ITransactionObject GetTransactionObject();
    }
}
