using Cardcraft.Microservice.aCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.RequestModels
{
    public class PurchaseCreditResponse : APIResponse<int>
    {
        public PurchaseCreditResponse(bool success, string apiStatus, string message, int data) 
            : base(success, apiStatus, message, data)
        {
        }
    }
}
