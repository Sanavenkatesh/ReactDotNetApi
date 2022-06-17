using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactDotNetApi.Data;
using ReactDotNetApi.Helpers;
using ReactDotNetApi.ModelDtos;
using ReactDotNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AccountController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto register)
        {
            if (_userRepository.GetUserByEmail(register.Email) != null)
            {
                return BadRequest(new { message = "Email already exists." });
            }
            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Password = register.Password
            };
            _userRepository.Create(user);

            return Ok(new { response = "success" });
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto login)
        {
            var user = _userRepository.GetUserByEmail(login.Email);
            if (user == null) return BadRequest(new { message = "Invalid Email" });
            if(user.Password != login.Password) return BadRequest(new { message = "Invalid Password" });

            var jwt = _jwtService.Generate(user.Id);
            var cookieAttributes = login.RememberMe == true ? new CookieOptions { Expires = DateTime.Now.AddDays(5) } : new CookieOptions { };
            Response.Cookies.Append("jwt", jwt, cookieAttributes);
            Response.Cookies.Append("name", user.FirstName +" "+ user.LastName, cookieAttributes);
            Response.Cookies.Append("email", user.Email, cookieAttributes);

            return Ok(new { jwt = jwt });
        }

        public IActionResult Authenticate()
        {
            var jwt = Request.Cookies["jwt"];
            try
            {
                if (jwt == null)
                {
                    return Unauthorized();
                }
                var token = _jwtService.VerifyUser(jwt);
                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userId);

                return Ok(user);
            }
            catch(Exception e)
            {
                return Unauthorized(e);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("email");
            return Ok(new { response = "success" });
        }
    }
}
