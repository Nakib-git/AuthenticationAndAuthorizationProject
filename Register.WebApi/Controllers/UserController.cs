using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Register.Application.Service.IContracts;
using Register.Domain.Models;
using Register.WebApi.ViewModel;

namespace Register.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class UserController : ControllerBase {
        private readonly IMapper _iMapper;
        public UserController(IMapper iMapper) {
            _iMapper = iMapper;
        }
        [AllowAnonymous]
        [HttpPost("register-user")]
        public IActionResult Post(UserViewModel userViewModel, [FromServices] IUserService userService) {
            if (userService.GetByEmail(userViewModel.Email) != null) {
                return Ok(ResponseDto.SetResponseDto(401, "Already Exist"));
            }
            var user = _iMapper.Map<User>(userViewModel);
            var isAdded = userService.Add(user);
            return Ok(ResponseDto.SetResponseDto(200, isAdded));
        }

        [HttpPut("update-user")]
        public IActionResult Put(UserViewModel userViewModel, [FromServices] IUserService userService) {
            if (userService.GetByEmailWithOutId(userViewModel.Email, userViewModel.Id) != null) {
                return Ok(ResponseDto.SetResponseDto(401, "Already Exist"));
            }
            var user = _iMapper.Map<User>(userViewModel);
            var isUpdated = userService.Update(user);
            return Ok(ResponseDto.SetResponseDto(200, isUpdated));
        }

        [HttpGet("all-users")]
        public IActionResult GetAll([FromServices] IUserService userService) {
            var userList = userService.GetAll();
            var userViewModelList = new List<UserViewModel>();
            userList.ToList()?.ForEach(user => {
                var userViewModel = _iMapper.Map<UserViewModel>(user);
                userViewModel.RoleName = user.Role?.Name;
                userViewModelList.Add(userViewModel);
            });
            return Ok(ResponseDto.SetResponseDto(200, userViewModelList));

        }
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id, [FromServices] IUserService userService) {
            var user = userService.GetById(id);
            var userViewModel = _iMapper.Map<UserViewModel>(user);
            return Ok(ResponseDto.SetResponseDto(200, userViewModel));
        }
        [HttpDelete("delete/{id}")]
        public IActionResult UserDelete(int id, [FromServices] IUserService userService) {
            var user = new User();
            user = userService.GetById(id);
            if (user == null) {
                return Ok(ResponseDto.SetResponseDto(401, "User not found"));
            }
            var isDeleted = userService.Remove(user);
            return Ok(ResponseDto.SetResponseDto(200, isDeleted));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(Login user, [FromServices] IAuthenticationService authenticationService, [FromServices] IUserService userService) {
            var userAvailable = userService.GetByEmailAndPassword(user.Email, user.Password);
            if (userAvailable == null) {
                return Ok(ResponseDto.SetResponseDto(401));
            }
            var token = authenticationService.Authenticate(user);
            return Ok(ResponseDto.SetResponseDto(200, token));
        }

    }
}
public class ResponseDto {
    public int Status { get; set; }
    public Object? Data { get; set; }

    public static ResponseDto SetResponseDto(int status, Object? data = null) {
        return new ResponseDto { Status = status, Data = data };
    }
}
