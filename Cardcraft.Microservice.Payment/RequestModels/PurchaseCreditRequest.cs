using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.RequestModels
{
    public class PurchaseCreditRequest
    {
        public string Token { get; set; }
        public int BillingAmountId { get; set; }
    }
}
