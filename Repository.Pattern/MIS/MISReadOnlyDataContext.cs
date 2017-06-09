using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;

namespace Repository.Pattern.MIS
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
