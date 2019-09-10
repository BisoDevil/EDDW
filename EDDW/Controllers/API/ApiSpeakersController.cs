using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDDW.Data;
using EDDW.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace EDDW.Controllers.API
{
    [Route("api/Speaker")]
    [ApiController]
    public class ApiSpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public ApiSpeakersController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/ApiSpeakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speaker>>> GetSpeaker()
        {
            return await _context.Speaker.ToListAsync();
        }

        // GET: api/ApiSpeakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Speaker>> GetSpeaker(Guid id)
        {
            var speaker = await _context.Speaker.FindAsync(id);

            if (speaker == null)
            {
                return NotFound();
            }

            return speaker;
        }

        // PUT: api/ApiSpeakers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker(Guid id, Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return BadRequest();
            }

            _context.Entry(speaker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // POST: api/ApiSpeakers
        [HttpPost]
        public async Task<ActionResult<Speaker>> PostSpeaker(Speaker speaker)
        {
            _context.Speaker.Add(speaker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeaker", new { id = speaker.Id }, speaker);
        }

        // DELETE: api/ApiSpeakers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Speaker>> DeleteSpeaker(Guid id)
        {
            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }

            _context.Speaker.Remove(speaker);
            await _context.SaveChangesAsync();

            return speaker;
        }

        [HttpGet("{Authenticate}/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateSpeaker([FromRoute] string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundStaff = await _context.Speaker.FirstOrDefaultAsync(s => s.Username == userName);
            string token = GenerateJSONWebToken(foundStaff);
            foundStaff.Token = token;

            return Ok(foundStaff);
        }




        private string GenerateJSONWebToken(Speaker userInfo)
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




        private bool SpeakerExists(Guid id)
        {
            return _context.Speaker.Any(e => e.Id == id);
        }
    }
}
