using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cardcraft.Microservice.aCore;

namespace Cardcraft.Microservice.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiControllerBase
    {
        public PaymentController(ILogger<PaymentController> logger):
            base(logger)
        {

        }

    }
}
