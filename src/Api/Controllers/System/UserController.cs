#region License
//
// Copyright (c) 2020, John Cottrell <me@john.co.com>
//
// This file is part of Jot.
//
// Jot is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Jot is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Jot.  If not, see <https://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jotfi.Jot.Api.Classes;
using jotfi.Jot.Api.Controllers.Base;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.System;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace jotfi.Jot.Api.Controllers.System
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController<UserController, UserService>
    {

        public UserController(IServiceProvider services) : base(services)
        {
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Authenticate model)
        {
            try
            {
                var user = Service.Authenticate(model.Username, model.Password);
                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
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
            catch (Exception ex)
            {
                Log.LogError(ex, ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: user
        [HttpGet]
        [AllowAnonymous]
        [EnableQuery()]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            var res = await Service.GetAllAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        // GET: user/5
        [HttpGet("{id:int?}")]
        [AllowAnonymous]
        [EnableQuery()]
        public async Task<ActionResult> GetUserById(long? id, ODataQueryOptions<User> odataQueryOptions)
        {
            var predicate = odataQueryOptions.GetFilter();
            var user = await Service.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetUserByName(string name)
        {
            var user = await Service.FirstOrDefaultAsync(p => p.UserName == name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("select")]
        [EnableQuery()]
        public async Task<ActionResult> Select(ODataQueryOptions<User> odataQueryOptions)
        {
            var predicate = odataQueryOptions.GetFilter();
            var user = await Service.SelectAsync(predicate);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        //[HttpPost("find")]
        //public async Task<ActionResult> GetUser([FromBody]object id)
        //{
        //    using var context = GetContext();
        //    var repository = new Repository<User>(context.UnitOfWork);
        //    var user = await repository.GetAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        // POST: user
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> PostUser(User user)
        {
            var userId = Convert.ToInt64(await Service.InsertAsync(user));
            if (userId == 0)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetUser", new { id = userId }, user);
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
