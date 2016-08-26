using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.ViewModels
{
    [DataContract]
    public class CompositeType2
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        [StringLength(10)]
        [Required]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
