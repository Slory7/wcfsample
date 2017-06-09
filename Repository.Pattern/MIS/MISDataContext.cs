using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;

namespace Repository.Pattern.MIS
{
    public class MISDataContext : PetaDataContext
    {
        public MISDataContext() 
            : base(MISConnection.ConnectionString, MISConnection.DefaultDbProviderName)
        {
        }        
    }
}
