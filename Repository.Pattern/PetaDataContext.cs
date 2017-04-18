using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;
using Repository.Pattern.NIS;
using System.Collections.Generic;

namespace Repository.Pattern
{
    public class PetaDataContext : PetaPoco.Database, IDataContext, IReadOnlyDataContext
    {
        public PetaDataContext(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }
        public Paged<T> Paged<T>(long page, long itemsPerPage, string sql, params object[] args)
        {
            var result = this.Page<T>(page, itemsPerPage, sql, args);
            var objPaged = new Paged<T>()
            {
                Context = result.Context,
                CurrentPage = result.CurrentPage,
                Items = result.Items,
                ItemsPerPage = result.ItemsPerPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
            return objPaged;
        }
    }
}
