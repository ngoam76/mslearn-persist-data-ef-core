// This code adds an api/coupon endpoint to the API.

using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponController : ControllerBase
{
    PromotionsContext _context;

    public CouponController(PromotionsContext context) // is injected into the constructor
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Coupon> Get() // returns all the coupons
    {
        return _context.Coupons
            .AsNoTracking()
            .ToList();
    }
}