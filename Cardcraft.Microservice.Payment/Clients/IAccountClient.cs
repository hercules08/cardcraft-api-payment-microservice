using Cardcraft.Microservice.aCore;
using Cardcraft.Microservice.Payment.RequestModels;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Clients
{
    public interface IAccountClient
    {
        Task<IAPIResponse> UpdateUserCredits(UpdateUserCreditRequest request);
    }
}
