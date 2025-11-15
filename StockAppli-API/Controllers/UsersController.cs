using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockAppli.Logger;
using StockAppli.Models.Entities;
using StockAppli.Models.Models;
using StockAppli_API.Services.Users;

namespace StockAppli_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService iUserService)
        {
            _userService = iUserService;
        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserModel user)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {

                var userExists = await _userService.UserExists(user.Email);
                if (userExists == null)
                {
                    var userCreated = await _userService.CreateUser(user);
                    if (userCreated != null)
                    {
                        var userlogin = await _userService.Login(user.Email.ToLower(), user.Password);
                        if (userlogin == null)
                            return BadRequest();
                        var token = await _userService.CreateToken(userlogin);

                        return Ok(new { token });
                    }
                    return BadRequest("Une erreur s'est produite !");
                }
                return BadRequest("Cet email existe déjà!");
            }
            catch (Exception ex)
            {
                NLogManager.Error(ex.Message, ex, nameof(UsersController), nameof(CreateUser));
                // SentrySdk.CaptureException(ex);
                return BadRequest("Une erreur s'est produite !");
            }
            finally
            {
                stopWatch.Stop();
                NLogManager.Trace($"{nameof(CreateUser)} Task took {stopWatch.ElapsedMilliseconds} Milliseconds", nameof(UsersController), nameof(CreateUser));
            }
        }
        [HttpGet("FindUserByEmail/{email}")]
        public async Task<ActionResult<User>> FindUserByEmail(string email)
        {
            var stopWatch = Stopwatch.StartNew();

            try
            {
                var user = await _userService.UserExists(email);
                if (user != null)
                {
                    return Ok(user);
                }

                return Ok(new User());
            }
            catch (Exception ex)
            {
                NLogManager.Error(ex.Message, ex, nameof(UsersController), nameof(FindUserByEmail));
                //SentrySdk.CaptureException(ex);
                return BadRequest(ex.Message);
            }
            finally
            {
                stopWatch.Stop();
                NLogManager.Trace($"{nameof(FindUserByEmail)} Task took {stopWatch.ElapsedMilliseconds} Milliseconds", nameof(UsersController), nameof(FindUserByEmail));
            }
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            try
            {
                bool result = await _userService.DeleteUser(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                NLogManager.Error(ex.Message, ex, nameof(UsersController), nameof(DeleteUser));
             //   SentrySdk.CaptureException(ex);
                return BadRequest(ex.Message);
            }
            finally
            {
                stopWatch.Stop();
                NLogManager.Trace($"{nameof(UsersController)} Task took {stopWatch.ElapsedMilliseconds} Milliseconds", nameof(UsersController), nameof(DeleteUser));
            }
        }
    }
}
