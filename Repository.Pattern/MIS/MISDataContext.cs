using Repository.Pattern.Interface;
using System;
using System.Data;
using PetaPoco;
using Repository.Pattern.NIS;
using System.Collections.Generic;

namespace Repository.Pattern.NIS
{
    public class MISDataContext : PetaDataContext
    {
        public MISDataContext() 
            : base(MISConnection.ConnectionString, MISConnection.DefaultDbProviderName)
        {
        }        
    }
}
