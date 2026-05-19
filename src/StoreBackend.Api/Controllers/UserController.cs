using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Facade;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Mappers;
using StoreBackend.Api.Mappers;
using StoreBackend.Exceptions;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;
using StoreBackend.Api.Security;
using Microsoft.AspNetCore.Authorization;





namespace StoreBackend.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserFacade userFacade) : ControllerBase
    {
        /*
        private readonly IUserFacade userFacade;

        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }*/
        [Authorize(Policy = AuthorizationPolicies.CanSearchUsers)]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await userFacade.GetAllAsync();
            var models = UserMapper.ToModel(users);
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await userFacade.GetByIdAsync(id);
                var model = UserMapper.ToModel(user);
                return Ok(model);
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }

       [Authorize(Roles = RoleNames.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequestModel user)
        {
            try
            {
                var requestDto = UserMapper.ToDto(user);
                var userDto = await userFacade.AddAsync(requestDto);
                var userModel = UserMapper.ToModel(userDto);
                return Ok(userModel);
            }
            catch (Exceptions.BadRequestResponseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await userFacade.DeleteAsync(id);
                return Ok();
            }
            catch (ResourceNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = RoleNames.Administrator)]
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRolesAsync(Guid userId)
        {
            var userRoles = await userFacade.GetUserRolesAsync(userId);
            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpPut("{userId}/roles")]
        public async Task<IActionResult> UpdateUserRolesAsync(Guid userId, [FromBody] UpdateRolesRequestModel model)
        {
            var requestDto = UserMapper.ToDto(model);
            var userRoles = await userFacade.UpdateUserRolesAsync(userId, requestDto);
            var responseModel = UserMapper.ToUserRolesResponseModel(userRoles);
            return Ok(responseModel);
        }

        [Authorize(Roles = RoleNames.Administrator)]
        [HttpDelete("{userId}/roles")]
        public async Task<IActionResult> DeleteUserRolesAsync(Guid userId)
        {

            await userFacade.DeleteUserRolesAsync(userId);
            return Ok();
        }


    }
}
