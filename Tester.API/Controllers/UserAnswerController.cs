using System.Net;
using AutoMapper;
using Tester.Shared.DTOs.UserAnswerDTOs;
using Tester.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Tester.Shared.DTOs.UserAswerDTOs;

namespace Tester.API.Controllers;

[ApiController]
[Route("api/user_answers")]
public class UserAnswersController : ControllerBase
{
    private readonly IUserAnswerService _service;
    private readonly IMapper _mapper;

    public UserAnswersController(IUserAnswerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<ApiResponse>> GetAnswerByUser(int userId)
    {
        var response = new ApiResponse();
        try
        {
            var models = await _service.GetUserAnswersAsync(userId);
            var dtos = _mapper.Map<IEnumerable<UserAnswerResponse>>(models);

            response.StatusCode = HttpStatusCode.OK;
            response.Result = dtos;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ErrorMessages.Add(ex.Message);
            return StatusCode((int)response.StatusCode, response);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Submit([FromBody] SubmitUserAnswerRequest dto)
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
            var model = _mapper.Map<UserAnswerModel>(dto);
            model.SubmittedDate = DateTime.UtcNow;

            await _service.SaveUserAnswerAsync(model);

            response.StatusCode = HttpStatusCode.Created;
            return StatusCode((int)response.StatusCode, response);
        }
        catch (KeyNotFoundException knf)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.ErrorMessages.Add(knf.Message);
            return NotFound(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ErrorMessages.Add(ex.Message);
            return StatusCode((int)response.StatusCode, response);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse>> UpdateAnswer([FromBody] UpdateUserAnswerRequest dto)
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
            var model = _mapper.Map<UserAnswerModel>(dto);
            model.SubmittedDate = DateTime.UtcNow;

            await _service.UpdateUserAnswerAsync(model);

            response.StatusCode = HttpStatusCode.NoContent;
            return StatusCode((int)response.StatusCode, response);
        }
        catch (KeyNotFoundException knf)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.ErrorMessages.Add(knf.Message);
            return NotFound(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.ErrorMessages.Add(ex.Message);
            return StatusCode((int)response.StatusCode, response);
        }
    }

}

