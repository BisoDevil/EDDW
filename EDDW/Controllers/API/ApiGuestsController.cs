using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDDW.Data;
using EDDW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EDDW.Controllers.API
{
    [Route("api/Guest")]
    [ApiController]
    public class ApiGuestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public ApiGuestsController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        // GET: api/ApiGuests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuest()
        {
            return await _context.Guest.ToListAsync();
        }

        // GET: api/ApiGuests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(Guid id)
        {
            var guest = await _context.Guest.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }

        // PUT: api/ApiGuests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(Guid id, Guest guest)
        {
            if (id != guest.Id)
            {
                return BadRequest();
            }

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("{Authenticate}/{userName}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(string userName, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var client = await _context.Guest.FirstAsync(g => g.Username == userName);
            if (client != null && client.Password == password)
            {
                string token = GenerateJSONWebToken(client);
                client.AccessToken = token;
                return Ok(client);
            }
            else
            {

                return BadRequest();
            }

        }





        // POST: api/ApiGuests
        [HttpPost]
        public async Task<ActionResult<Guest>> PostGuest(Guest guest)
        {

            guest.Username = GetUserName(guest);
            _context.Guest.Add(guest);
            await _context.SaveChangesAsync();
            string token = GenerateJSONWebToken(guest);
            guest.AccessToken = token;

            return CreatedAtAction("GetGuest", new { id = guest.Id }, guest);
        }

        // DELETE: api/ApiGuests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guest>> DeleteGuest(Guid id)
        {
            var guest = await _context.Guest.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.Guest.Remove(guest);
            await _context.SaveChangesAsync();

            return guest;
        }

        private bool GuestExists(Guid id)
        {
            return _context.Guest.Any(e => e.Id == id);
        }

        private string GetUserName(Guest guest)
        {
            string iso = guest.Country.Split("-")[1];
            int end = new Random().Next(1000, 9999);
            string cap = guest.Title.ToString().ToUpper();
            return $"{cap}{iso}{end}";
        }


        private string GenerateJSONWebToken(Guest userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.Fullname),

        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())   };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddSeconds(30),
              signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }




}
