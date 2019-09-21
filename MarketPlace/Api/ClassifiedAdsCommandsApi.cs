using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Api
{
    [Route("/ad")]
    public class ClassifiedAdsCommandsApi: Controller
    {
        [HttpPost]
        public async Task<ActionResult> Post(Contracts.ClassifiedAds.V1.Create request)
        {
            return Ok();
        }
    }
}