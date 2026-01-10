using DataWarehouseApi.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataWarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IConfiguration configuration, Goldcontext dbContext) : ControllerBase
    {
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto registerRequest)
        {
            var user = new User();
            var hashedPassword = new PasswordHasher<User>()
                 .HashPassword(user, registerRequest.password);

            user.username = registerRequest.username;
            user.passwordHash = hashedPassword;

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();



            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.username == request.username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            //Check if the passwrord from the login request and database is the same
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.passwordHash, request.password)
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong username or password");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        //create a JWT Token for the logged in user 
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //Jwt token creation, the header, payload and signature
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"), //who is issuing the token
                audience: configuration.GetValue<string>("AppSettings:Audience"), //who is the users using the token
                claims: claims,  //these are claims of who the requester is claiming to be
                expires: DateTime.UtcNow.AddDays(1),  //token expiration, in this case in 24 hours
                signingCredentials: creds  //key and algorithm used to sign the token
            );

            //converts the JWTSecurityToken to a string so it can be a format that can be sent and usable over HTTP
            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return jwt;
        }

        [Authorize]
        [HttpGet]
        [Route("databaseaccess")]
        public IActionResult AccessDatabaseStatus()
        {
            return Ok("You are now authenticated :)");
        }
    }
}
