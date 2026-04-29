using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBackend.Facade;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Mappers;
using StoreBackend.Exceptions;
using StoreBackend.Domain.Entities;
using StoreBackend.DomainService;



namespace StoreBackend.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserFacade userFacade;

        public UserController(IUserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

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


    }
}
