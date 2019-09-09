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
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EDDW.Controllers.API
{
    [Route("api/Employee")]
    [ApiController]
    public class ApiEmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config = null;
        public ApiEmployeesController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/ApiEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: api/ApiEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/ApiEmployees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/ApiEmployees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/ApiEmployees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        [HttpGet("{Authenticate}/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateSpeaker([FromRoute] string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var foundStaff = await _context.Employee.FirstOrDefaultAsync(s => s.Username == userName);
            string token = GenerateJSONWebToken(foundStaff);
            foundStaff.Token = token;

            return Ok(foundStaff);
        }




        private string GenerateJSONWebToken(Employee userInfo)
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





        private bool EmployeeExists(Guid id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
