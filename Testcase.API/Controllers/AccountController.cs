using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testcase.Infrastructure.Concrete.Interfaces;
using Testcase.User.Domain;
using Testcase.User.Domain.Entites;
using Testcase.User.Domain.Repositories;

namespace Testcase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _passwordHashing;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserRepository userRepository;

        public AccountController(IMapper mapper, IPasswordHashing passwordHashing, ITokenHandler tokenHandler, IUserRepository userRepository)
        {
            _mapper = mapper;
            _passwordHashing = passwordHashing;
            _tokenHandler = tokenHandler;
            this.userRepository = userRepository;
        }

        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public ActionResult Login(TokenRequestModel requestData)
        {
            AccessToken token = null;
            Users existUser = userRepository.GetUserByUserName(requestData.username).Result;
            if (existUser == null)
            {
                return Unauthorized("invalid user email.");
            }

            if (!_passwordHashing.VerifyPassword(requestData.password, existUser.Password))
            {
                return Unauthorized("invalid user password.");
            }
            token = _tokenHandler.CreateToken(requestData, new List<OperationClaim>() { new OperationClaim() { Name = "user" } });



            if (token == null)
                return BadRequest((AccessToken)null);

            return Ok(token);
        }
    }
}
