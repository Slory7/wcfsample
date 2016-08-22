using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Business
{
    public class DataProcessor : IDataProcessor
    {
        public CompositeType2 Process(CompositeType2 composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }

            return composite;
        }
    }
}