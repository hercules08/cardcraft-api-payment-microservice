using Cardcraft.Microservice.aCore;
using Cardcraft.Microservice.Payment.RequestModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<IAPIResponse> UpdateUserCredits(UpdateUserCreditRequest request)
        {
            HttpResponseMessage response = null;

            try
            {
                response  = await UpdateUserCreditsFromAccountService(request);
            }
            catch(Exception ex)
            {

            }

            if (response != null && response.IsSuccessStatusCode)
            {

                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse<UpdateUserCreditResponse>>(jsonString);
                return apiResponse;
            }
            else
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<APIResponse>(jsonString);
                return apiResponse;
            }
        }

        private async Task<HttpResponseMessage> UpdateUserCreditsFromAccountService(UpdateUserCreditRequest request)
        {
            var accountServiceBaseUrl = _configuration["AccountServiceBaseUrl"];
            var updateUserCreditResource = $"api/account/updateusercredits";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
                httpClient.BaseAddress = new Uri(accountServiceBaseUrl);
                return await httpClient.PostAsJsonAsync<UpdateUserCreditRequest>(updateUserCreditResource, request);
            }
        }
    }
}
