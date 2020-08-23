using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using DnsClient.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> logger;
        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
             this.logger = logger;
            _productRepository = productRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);

        }
        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>>GetById(string id)
        {
            var product =await _productRepository.GetProduct(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                logger.LogError($"Product with Id {id},not found.");
                return NotFound();
            }
        }
        [HttpGet("[action]/{category}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IEnumerable<Product>>>GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);
            if(products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]

        public async Task<ActionResult>CreateProduct([FromBody]Product product)
        {
          var created =  await _productRepository.Create(product);
            if (created != null)
            {
                return CreatedAtRoute("GetProduct",new { id = product.Id } ,product);
            }
            else
            {
                return BadRequest();
            }
           
            

        }
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<Product>>Update(Product product)
        {
            
            if (product != null)
            {
                await _productRepository.Update(product);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}:length(24)")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult>Delete(string id)
        {
            var deletedEntity = await _productRepository.GetProduct(id);
            if (deletedEntity != null)
            {
                await _productRepository.Delete(deletedEntity);
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }



    }
}
