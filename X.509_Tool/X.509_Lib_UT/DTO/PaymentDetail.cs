
using System.Diagnostics.CodeAnalysis;

namespace X._509_Lib_IT.DTO
{
    [ExcludeFromCodeCoverage]
    public class PaymentDetail
    {
        public string PaymentMethod { get; set; }
        public string BudgetCenterNumber { get; set; }
        public string AgentWritingNumber { get; set; }
    }
}
