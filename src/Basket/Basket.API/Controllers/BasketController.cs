using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(BasketCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>>GetBasket(string userName)
        {
            return Ok( await _basketRepository.GetBasket(userName));
        }
        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]

        public async Task<IActionResult>DeleteBasket(string userName)
        {
            return Ok(await _basketRepository.DeleteBasket(userName)); 
                
        }
        [HttpPost]
        [ProducesResponseType(typeof(BasketCart), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody]BasketCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));
        }
    }
}
