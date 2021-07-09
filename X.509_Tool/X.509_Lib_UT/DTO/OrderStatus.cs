
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    [ExcludeFromCodeCoverage]
    public class OrderStatus
    {
        public long OrderId { get; set; }
        public string CustNo { get; set; }
        public string Message { get; set; }
        public string OrderDate { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public List<OrderItem> OrderStatusDetail { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class OrderItem
    {
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int OrderQuantity { get; set; }
        public string State { get; set; }
        public int? ShippedQuanity { get; set; }
        public string TrackingNo { get; set; }
        public string TrackingNoUrl { get; set; }
    }
}
