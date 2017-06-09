using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Pattern.MIS
{
    public class MISConnection
    {
        private readonly static string m_connString;
        private readonly static string m_DbProviderName;

        static MISConnection()
        {
            m_connString = System.Configuration.ConfigurationManager.ConnectionStrings["MISDBContext"].ConnectionString;

            m_DbProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["MISDBContext"].ProviderName;
        }

        public static string ConnectionString
        {
            get { return m_connString; }
        }
        public static string DefaultDbProviderName
        {
            get { return m_DbProviderName; }
        }
    }
}
