using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Testcase.API.DTOs;
using Testcase.Infrastructure.Concrete.Interfaces;
using Testcase.User.Domain;
using Testcase.User.Domain.Entites;
using Testcase.User.Domain.Repositories;
using Testcase.User.Domain.Responses;

namespace Testcase.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHashing _passwordHashing;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserRepository _userRepository;

        public AccountController(IMapper mapper, IPasswordHashing passwordHashing, ITokenHandler tokenHandler, IUserRepository userRepository)
        {
            _mapper = mapper;
            _passwordHashing = passwordHashing;
            _tokenHandler = tokenHandler;
            this._userRepository = userRepository;
        }

        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public ActionResult Login(TokenRequestModel requestData)
        {
            AccessToken token = null;
            APIResponse<Users> existUser = _userRepository.GetUserByUserName(requestData.username).Result;
            if (!existUser.IsSuccess)
                return NotFound(existUser.ErrorMessage);
            if (existUser.Data == null)
                return Unauthorized("invalid username.");
            

            if (!_passwordHashing.VerifyPassword(requestData.password, existUser.Data.Password)) 
                return Unauthorized("invalid user password.");
           
            token = _tokenHandler.CreateToken(requestData, new List<OperationClaim>() { new OperationClaim() { Name = "user" } });
            if (token == null)
                return BadRequest((AccessToken)null);

            return Ok(token);
        }
       
        [ProducesResponseType(typeof(APIResponse<UserDto>), StatusCodes.Status200OK)]
        [HttpPost("register")]
        public IActionResult Register(UserDto user)
        {
            APIResponse<UserDto> result = new APIResponse<UserDto>();
            #region check User Is Exists
            APIResponse existUser = _userRepository.CheckUserByExist(user.UserName).Result;
            if(!existUser.IsSuccess)
                return BadRequest(existUser.ErrorMessage);
            if (existUser.AlreadyExist)
            {
                result.SetFailure("Username  is already exists.");
                return Ok(result);
            }
            #endregion

            Users userEntity = _mapper.Map<UserDto, Users>(user);
            userEntity.Password = _passwordHashing.HashPassword(user.Password);
            _userRepository.Create(userEntity);
            result.ObjectId = userEntity.UserId;
            result.SetData(_mapper.Map<Users,UserDto>(userEntity));
            return Ok(result);

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(APIResponse<UserDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult GetUserByUserName(string userName)
        {
            APIResponse<UserDto> result = new APIResponse<UserDto>();
            APIResponse<Users> user = _userRepository.GetUserByUserName(userName).Result;
            result.ObjectId = user.Data.UserId;
            if (!user.IsSuccess)
                return NotFound(user.ErrorMessage);
            if (user.Data == null)
                return Unauthorized("invalid username.");

            result.SetData(_mapper.Map<Users, UserDto>(user.Data));
            return Ok(result);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(APIResponse<UserDto>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            APIResponse<UserDto> result = new APIResponse<UserDto>();
            APIResponse<Users> user = _userRepository.GetUser(id).Result;
            result.ObjectId = user.Data.UserId;
            if (!user.IsSuccess)
                return NotFound(user.ErrorMessage);
            if (user.Data == null)
                return Unauthorized("invalid username.");

            result.SetData(_mapper.Map<Users, UserDto>(user.Data));
            return Ok(result);
        }
    }
}
