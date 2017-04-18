using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;
using Repository.Pattern.NIS;
using System.Collections.Generic;

namespace Repository.Pattern.NIS
{
    public class MISReadOnlyDataContext : PetaDataContext
    {
        readonly static string _connectionString;
        static MISReadOnlyDataContext()
        {
            _connectionString = MISConnection.ConnectionString.TrimEnd(';');
            _connectionString += ";applicationintent=readonly";
        }
        public MISReadOnlyDataContext()
            : base(_connectionString, MISConnection.DefaultDbProviderName)
        {
        }
    }
}
