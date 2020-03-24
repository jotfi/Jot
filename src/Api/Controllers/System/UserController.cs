using System.Collections;
using System.Threading.Tasks;
using jotfi.Jot.Api.Controllers.Base;
using jotfi.Jot.Core.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.System
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(UserViewModel viewmodel) : base(viewmodel)
        {
            
        }

        UserViewModel GetViewModel() => (UserViewModel)ViewModel;        

        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var res = await GetViewModel().GetUsersAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }


        // GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetProducts(int id)
        //{
        //    var products = await _context.Products.FindAsync(id);

        //    if (products == null)
        //    {
        //        return NotFound();
        //    }

        //    return products;
        //}

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProducts(int id, Products products)
        //{
        //    if (id != products.ProductId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(products).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult> PostProducts(Products products)
        //{
        //    _context.Products.Add(products);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        //}

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteProducts(int id)
        //{
        //    var products = await _context.Products.FindAsync(id);
        //    if (products == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(products);
        //    await _context.SaveChangesAsync();

        //    return products;
        //}

        //private bool ProductsExists(int id)
        //{
        //    return _context.Products.Any(e => e.ProductId == id);
        //}
    }
}
