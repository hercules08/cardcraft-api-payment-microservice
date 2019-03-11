using Cardcraft.Microservice.Payment.RequestModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Clients
{
    public class AccountClient : IAccountClient
    {
        private IConfiguration _configuration;

        public AccountClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> UpdateUserCredits(UpdateUserCreditRequest request)
        {
            HttpResponseMessage response = null;

            try
            {
                response  = await UpdateUserCreditsFromAccountService(request);
            }
            catch(Exception ex)
            {

            }

            if(response != null && response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        private async Task<HttpResponseMessage> UpdateUserCreditsFromAccountService(UpdateUserCreditRequest request)
        {
            var accountServiceBaseUrl = _configuration["AccountServiceBaseUrl"];
            var updateUserCreditResource = $"api/account/updateusercredits";
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(accountServiceBaseUrl);
                return await httpClient.PostAsJsonAsync<UpdateUserCreditRequest>(updateUserCreditResource, request);
            }
        }
    }
}
