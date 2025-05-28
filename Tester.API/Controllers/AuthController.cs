using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Shared.DTOs;
using Tester.Shared.DTOs.AuthDTOs;

namespace Tester.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterRequest request)
    {
        var response = new ApiResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.AddRange(ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return BadRequest(response);
        }

        try
        {
            var userModel = _mapper.Map<UserModel>(request);
            await _userService.RegisterAsync(userModel);
            response.StatusCode = HttpStatusCode.Created;
            return StatusCode((int)response.StatusCode, response);
        }
        catch (InvalidOperationException ioex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.Conflict;
            response.ErrorMessages.Add(ioex.Message);
            return Conflict(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ErrorMessages.Add("An unexpected error occurred during registration.");
            return StatusCode((int)response.StatusCode, response);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginRequest request)
    {
        var response = new ApiResponse();

        if (!ModelState.IsValid)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.ErrorMessages.AddRange(ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return BadRequest(response);
        }

        try
        {
            var loginModel = _mapper.Map<LoginModel>(request);
            var authResponse = await _userService.AuthenticateAsync(loginModel);

            if (authResponse == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ErrorMessages.Add("Invalid login or password.");
                return Unauthorized(response);
            }

            response.Result = authResponse;
            response.StatusCode = HttpStatusCode.OK;
            response.IsSuccess = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ErrorMessages.Add("An unexpected error occurred during login.");
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
