using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto.RequestDto;
using NZWalks.Repository.Interface;
using NZWalks.Repository.Repo;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICreateTokenRepository _repository;

        public AuthController(UserManager<IdentityUser> userManager, ICreateTokenRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered successfully");
                    }
                }
                
            }

            return BadRequest("Something Went wrong");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var identityResult = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if(identityResult != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(identityResult, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityResult);

                    if (roles != null)
                    {
                       var jwtToken = _repository.CreateJWTToken(identityResult, roles.ToList());

                        var responce = new LoginResponceDto
                        {
                            JwtToken = jwtToken,
                        };

                        return Ok(responce);
                    }
                     
                }
            }
            return BadRequest("invalid User Name Or Password");
        }
    }
}
