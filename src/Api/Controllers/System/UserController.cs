using System.Collections;
using System.Threading.Tasks;
using jotfi.Jot.Api.Controllers.Base;
using jotfi.Jot.Core.ViewModels.System;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jotfi.Jot.Api.Controllers.System
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController<UserViewModel>
    {
        public UserController(UserViewModel viewmodel) : base(viewmodel)
        {
            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Authenticate model)
        {
            var user = ViewModel.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        // GET: user
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var res = await ViewModel.GetUsersAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        // GET: user/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(long id)
        {
            var user = await ViewModel.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: user
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            var ok = await ViewModel.CreateUserAsync(user);
            if (!ok)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

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
