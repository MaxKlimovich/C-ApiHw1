using Microsoft.AspNetCore.Mvc;
using Ex1.Models;



namespace Ex1.Controllers;

[ApiController]
[Route("[controller]")]
    
public class ProductController : ControllerBase
{
    private readonly StoreContext context;

    public ProductController(StoreContext context)
    {
        this.context = context;
    }
    
    [HttpGet("getProduct")]
    public IActionResult GetProducts()
    {
        try
        {
            using (var context = new StoreContext())
            {
                var products = context.Products.Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                }).ToList();
                return Ok(products);
            }
        }
        catch
        {
            return StatusCode(500);
        } 
    }
    [HttpPost("putProduct")]
    public IActionResult PutProducts([FromQuery] string name, string description, int groupId, int price)
    {
        try
        {
            if (!context.Products.Any(x => x.Name.ToLower().Equals(name)))
                {
                    context.Add(new Product()
                    {
                        Name = name,
                        Description = description,
                        Price = price,
                    });
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return StatusCode(409);
                }
        }
        catch
        {
            return StatusCode(500);
        } 
    }
    
    [HttpDelete("deleteProduct/{productId}")]
    public IActionResult DeleteProducts(int productId)
    {
        try
        {
            var product = context.Products.Find(productId);
            
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch
        {
            return StatusCode(500);
        } 
    }
    
    [HttpDelete("deleteGroup/{productId}")]
    public IActionResult DeleteGroup(int groupId)
    {
        try
        {
            var group = context.Groups.Find(groupId);
            
            if (group != null)
            {
                var productInGroup = context.Products.Where(x => x.GroupID == groupId).ToList();
                if (productInGroup.Any())
                {
                    context.Products.RemoveRange(productInGroup);
                }

                context.Groups.Remove(group);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch
        {
            return StatusCode(500);
        } 
    }
    
    [HttpPut("updateProductPrice/{productId}")]
    public IActionResult UpdateProductPrice(int productId, [FromQuery] int newPrice)
    {
        try
        {
            var product = context.Products.Find(productId);
            
            if (product != null)
            {
                product.Price = newPrice;
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch
        {
            return StatusCode(500);
        } 
    }
} 

