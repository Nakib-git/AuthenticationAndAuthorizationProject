using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Register.Application.Service.IContracts;

namespace Register.WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class RoleController : ControllerBase {
        public RoleController() {
        }

        [HttpGet("all-roles")]
        public IActionResult GetAll([FromServices] IRoleService roleService) {
            var roleList = roleService.GetRoleList();
            return Ok(ResponseDto.SetResponseDto(200, roleList));

        }


    }
}
