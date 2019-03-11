using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Model
{
    public class BillingInfo
    {
        public int Id { get; set; }
        public float Cost { get; set; }
        public int CreditCount { get; set; }
    }
}
