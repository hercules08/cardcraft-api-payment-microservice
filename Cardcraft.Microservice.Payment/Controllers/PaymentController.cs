using Cardcraft.Microservice.Account.BusinessLogic.Interface;
using Cardcraft.Microservice.aCore;
using Cardcraft.Microservice.Payment.Clients;
using Cardcraft.Microservice.Payment.Model;
using Cardcraft.Microservice.Payment.RequestModels;
using Cardcraft.Microservice.Payment.Stubs;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [AllowAnonymous]
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
        [Route("PurchaseCreditsTest")]
        [ValidateModelState]
        public async Task<IActionResult> PurchaseCreditsTest([FromBody]PurchaseCreditRequest request)
        {
            BillingInfo billInfo =
                BillingsStub.BillingAmounts.Find(x => x.Id == request.BillingAmountId);

            var charge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(billInfo.Cost * 100),
                Currency = "usd",
                Description = "Cardcraft Purchase " + CONTEXT_USER,
                SourceId = request.Token
            };

            var service = new ChargeService("sk_test_yqhJJAmFkjInHP9tJL4gR6G3");

            Charge chargeResponse = null;

            try
            {
                chargeResponse = service.Create(charge);

                IAPIResponse updateUserCreditResponse = await _accountClient.UpdateUserCredits(new UpdateUserCreditRequest()
                {
                    UserProfileId = CONTEXT_USER,
                    AccessToken = CONTEXT_TOKEN,
                    NumOfCreditsToAdd = billInfo.CreditCount
                });

                if (updateUserCreditResponse.Success)
                {
                    APIResponse<UpdateUserCreditResponse> creditResponse = (APIResponse<UpdateUserCreditResponse>)updateUserCreditResponse;
                    PurchaseCreditResponse purchaseCreditResponse = new PurchaseCreditResponse(true, "successful_purchase"
                        , "Successfully added credits. Here are the total credits.", creditResponse.Data);

                    return Ok(purchaseCreditResponse);
                }
            }
            catch (StripeException ex)
            {
                StripeError stripeError = ex.StripeError;

                // Handle error
                return BadRequest(stripeError);
            }

            return BadRequest(new APIResponse(false, "unsuccessful_credit_add", "We were unable to add credits at the moment"));
        }

        /// <summary>
        /// Allows user to purchase credit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PurchaseCredits")]
        [ValidateModelState]
        public async Task<IActionResult> PurchaseCredits([FromBody]PurchaseCreditRequest request)
        {
            BillingInfo billInfo =
                BillingsStub.BillingAmounts.Find(x => x.Id == request.BillingAmountId);

            var charge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(billInfo.Cost * 100),
                Currency = "usd",
                Description = "Cardcraft Purchase " + CONTEXT_USER,
                SourceId = request.Token
            };

            var service = new ChargeService(_configuration["StripeKey"]);

            Charge chargeResponse = null;

            try
            {
                chargeResponse = service.Create(charge);

                IAPIResponse updateUserCreditResponse = await _accountClient.UpdateUserCredits(new UpdateUserCreditRequest()
                {
                    UserProfileId = CONTEXT_USER,
                    AccessToken = CONTEXT_TOKEN,
                    NumOfCreditsToAdd = billInfo.CreditCount
                });

                if (updateUserCreditResponse.Success)
                {
                    APIResponse<UpdateUserCreditResponse> creditResponse = (APIResponse<UpdateUserCreditResponse>)updateUserCreditResponse;
                    PurchaseCreditResponse purchaseCreditResponse = new PurchaseCreditResponse(true, "successful_purchase"
                        , "Successfully added credits. Here are the total credits.", creditResponse.Data);

                    return Ok(purchaseCreditResponse);
                }
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
        [AllowAnonymous]
        public ActionResult TestHealth(bool throwException)
        {
            if (throwException)
                throw new Exception();

            return Ok();
        }
    }
}
