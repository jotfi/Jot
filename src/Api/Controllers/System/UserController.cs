using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jotfi.Jot.Api.Controllers.Base;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace jotfi.Jot.Api.Controllers.System
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController<UserService>
    {
        public UserController(Core.Application app) : base(app, app.Services.System.User)
        {
            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Authenticate model)
        {
            var user = Service.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Service.App.AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("hash", user.Hash)
                    //new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return Ok(user);
        }

        // GET: user
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var res = await Service.GetUsersAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        // GET: user/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetUserById(long id)
        {
            var user = await Service.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var user = await Service.GetUserByNameAsync(name);
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
            var userId = await Service.CreateUserAsync(user);
            if (userId == 0)
            {
                return BadRequest();
            }
            var newUser = await Service.GetUserByIdAsync(userId);
            if (newUser == null)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetUser", new { id = userId }, newUser);
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
