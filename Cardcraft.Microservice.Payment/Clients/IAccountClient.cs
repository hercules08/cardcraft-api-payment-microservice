using Cardcraft.Microservice.Payment.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Clients
{
    public interface IAccountClient
    {
        Task<bool> UpdateUserCredits(UpdateUserCreditRequest request);
    }
}
