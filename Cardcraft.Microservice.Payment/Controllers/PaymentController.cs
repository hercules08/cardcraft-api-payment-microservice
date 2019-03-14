using Cardcraft.Microservice.Account.BusinessLogic.Interface;
using Cardcraft.Microservice.aCore;
using Cardcraft.Microservice.Payment.Clients;
using Cardcraft.Microservice.Payment.Model;
using Cardcraft.Microservice.Payment.RequestModels;
using Cardcraft.Microservice.Payment.Stubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiControllerBase
    {
        private static IConfiguration _configuration;
        private IAccountClient _accountClient;

        public PaymentController(IPaymentManager paymentManager
            , ILogger<PaymentController> logger
            , IConfiguration configuration
            , IAccountClient client)
            : base(paymentManager, logger)
        {
            _configuration = configuration;
            _accountClient = client;
        }

        /// <summary>
        /// Gets the Billing Amounts and Credit Count
        /// </summary>
        /// <returns>List of Billing Ids, Billing amount and credits that user will</returns>
        [HttpGet]
        [Route("GetBillingAmounts")]
        public ActionResult GetBillingAmounts()
        {
            return Ok(BillingsStub.BillingAmounts);
        }

        /// <summary>
        /// Allows user to purchase credit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PurchaseCredits")]
        public async Task<IActionResult> PurchaseCredits([FromBody]PurchaseCreditRequest request)
        {
            BillingInfo billInfo =
                BillingsStub.BillingAmounts.Find(x => x.Id == request.BillingAmountId);



            var charge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(billInfo.Cost * 100),
                Currency = "usd",
                Description = "Cardcraft Purchase " + request.UserId,
                SourceId = request.Token
            };

            var service = new ChargeService(_configuration["StripeKey"]);

            Charge chargeResponse = null;

            try
            {
                chargeResponse = service.Create(charge);
                bool success = await _accountClient.UpdateUserCredits(new UpdateUserCreditRequest()
                {
                    NumOfCreditsToAdd = billInfo.CreditCount,
                    UserProfileId = request.UserId
                });

                if (success)
                    return Ok(new PurchaseCreditResponse(true, "successful_purchase", "Successfully added credits", billInfo.CreditCount));
            }
            catch (StripeException ex)
            {
                StripeError stripeError = ex.StripeError;

                // Handle error
                return BadRequest(stripeError);
            }

            return BadRequest(new APIResponse(false, "unsuccessful_credit_add", "We were unable to add credits at the moment"));
        }

        [HttpGet]
        [Route("TestHealth")]
        public ActionResult TestHealth(bool throwException)
        {
            if (throwException)
                throw new Exception();

            return Ok();
        }
    }
}
