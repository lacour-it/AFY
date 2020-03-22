using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AFYAPI.Data;
using AFYAPI.Dtos;
using AFYAPI.Models;

namespace AFYAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private AFYContext _context;

        public UsersController(AFYContext Context)
        {
            _context = Context;
        }

        private bool AFYUserExists(int id)
        {
            return _context.Users.Any(e => e.AFYUserId == id);
        }

        /*[AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }*/
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _context.Users.Select(u => u.Username).ToList();
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            AFYUser user;
            UserDto userdto = new UserDto() { Password = password, Username = username };


            if (_context.Users.Any(u => u.Username == userdto.Username))
            {
                user = _context.Users.First(u => u.Username == userdto.Username);
                if (user.Password != userdto.Password)
                {
                    return BadRequest(new { message = "Password does not match" });
                }
            }
            else
            {
                return BadRequest(new { message = "Username does not exist" });
            }

            return Ok(new
            {
                Id = user.AFYUserId,
                Username = user.Username,
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDto userDto)
        {
            AFYUser user;
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            if (AFYUserExists(id))
            {
                user = _context.Users.First(u => u.Username == userDto.Username);
                _context.Entry(user).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYUserExists(id))
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

        // POST: api/AFYHolidays
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            AFYUser user;
            if (!_context.Users.Any(u => u.Username == userDto.Username))
            {
                user = new AFYUser();
                user.Username = userDto.Username;
                user.Password = userDto.Password;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest(new { message = "Username does already exist" });
            }
            userDto.Id = user.AFYUserId;
            return CreatedAtAction("GetAFYUser", new { id = user.AFYUserId }, userDto);
        }

        // DELETE: api/AFYHolidays/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYUser>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Username == "admin")
                return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /*[HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }*/

    }
}