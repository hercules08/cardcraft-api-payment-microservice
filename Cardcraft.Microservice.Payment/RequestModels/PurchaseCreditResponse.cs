using Cardcraft.Microservice.aCore;

namespace Cardcraft.Microservice.Payment.RequestModels
{
    public class PurchaseCreditResponse : APIResponse<UpdateUserCreditResponse>
    {
        public PurchaseCreditResponse(bool success, string apiStatus, string message, UpdateUserCreditResponse data) 
            : base(success, apiStatus, message, data)
        {
        }
    }
}
