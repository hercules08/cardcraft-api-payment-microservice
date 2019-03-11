using Cardcraft.Microservice.Payment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Stubs
{
    public class BillingsStub
    {
        public static List<BillingInfo> BillingAmounts
        {
            get
            {
                return new List<BillingInfo>
                {
                    new BillingInfo { Id = 1, Cost = 3.99f, CreditCount = 1 },
                    new BillingInfo { Id = 2, Cost = 9.99f, CreditCount = 3 },
                    new BillingInfo { Id = 3, Cost = 19.99f, CreditCount = 10 }
                };
            }
        }
    }
}
