using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Methods;
using CodeBook.Business.App.Services;
using CodeBook.Business.App.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AbstractValidator<LoginDto> _loginValidator;
        private readonly AbstractValidator<RegisterDto> _registerValidator;
        public AuthController(IAuthService authService, AbstractValidator<LoginDto> loginvalidator, AbstractValidator<RegisterDto> registervalidator)
        {   _authService = authService;
            _loginValidator = loginvalidator;
            _registerValidator = registervalidator;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto logininfo)
        {
            var validationResult = _loginValidator.Validate(logininfo);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (_authService.Login(logininfo))
            {
                return Ok(new { message = "Login Successful." });
            }
            return Unauthorized(new { message = "Invalid Email or Password" });

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto registerinfo)
        {
            var validationResult = _registerValidator.Validate(registerinfo);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if(_authService.Register(registerinfo))
            {
                return Created( "Email Already Registered!" , registerinfo);
            }
            return Conflict(new { message = "Email Already Registered!" });
        }
    }
}
