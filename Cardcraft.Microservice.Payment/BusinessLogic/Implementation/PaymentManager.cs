using Cardcraft.Microservice.Account.BusinessLogic.Interface;
using Cardcraft.Microservice.aCore;
using Microsoft.Extensions.Logging;

namespace Cardcraft.Microservice.Payment.BusinessLogic
{
    public class PaymentManager : ActionManagerBase, IPaymentManager
    {
        public PaymentManager(ILogger<PaymentManager> logger) : base(logger)
        {
        }
    }
}
