using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cardcraft.Microservice.Payment.RequestModels
{
    public class PurchaseCreditRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public int BillingAmountId { get; set; }
    }
}
