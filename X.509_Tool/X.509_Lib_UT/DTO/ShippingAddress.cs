using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X._509_Lib_IT.DTO
{
    [ExcludeFromCodeCoverage]
    public class ShippingAddress
    {
        public string WritingNo { get; set; }
        public string ProfileType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }      
    }
}
