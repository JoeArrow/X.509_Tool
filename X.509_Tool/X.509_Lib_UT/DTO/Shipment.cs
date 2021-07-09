
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    [ExcludeFromCodeCoverage]
    public class Shipment
    {
        public List<ShipmentType> ShipmentMethod { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ShipmentType
    {
        public string ShipmentMethodNo { get; set; }
        public string ShipmentMethodDesc { get; set; }
    }
}
