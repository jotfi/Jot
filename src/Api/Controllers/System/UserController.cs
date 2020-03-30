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
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jotfi.Jot.Api.Controllers.Base;
using jotfi.Jot.Base.Settings;
using jotfi.Jot.Core.Services.System;
using jotfi.Jot.Database.Classes;
using jotfi.Jot.Model.Base;
using jotfi.Jot.Model.Primitives;
using jotfi.Jot.Model.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jotfi.Jot.Api.Controllers.System
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService Users;
        private readonly AppSettings Settings;
        private readonly ILogger Log;

        public UserController(UserService users, 
            IOptions<AppSettings> settings,
            ILogger<UserService> log) : base(settings)
        {
            Users = users;
            Settings = settings.Value;
            Log = log;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Authenticate model)
        {
            var user = Users.Authenticate(model.Username, model.Password);
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

        // GET: user
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            using var context = GetContext();
            var repository = new Repository<User>(context.UnitOfWork);
            var res = await repository.GetAllAsync();
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
            using var context = GetContext();
            var repository = new Repository<User>(context.UnitOfWork);
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetUserByName(string name)
        {
            using var context = GetContext();
            var repository = new Repository<User>(context.UnitOfWork);
            var user = await repository.SelectAsync(p => p.UserName == name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.FirstOrDefault());
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
            var userId = await Users.CreateUserAsync(user);
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
