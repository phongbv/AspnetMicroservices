using System.Net;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> GetDiscount(string productName)
    {
        return Ok(await _repository.GetDiscount(productName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateDiscount([FromBody] Coupon coupon)
    {
        await _repository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateDiscount([FromBody] Coupon coupon)
    {
        await _repository.UpdateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteDiscount(string productName)
    {
        return Ok(await _repository.DeleteDiscount(productName));
    }
}