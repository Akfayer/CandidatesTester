using System.Net;
using AutoMapper;
using Tester.Shared.DTOs.AnswerOptionDTOs;
using Tester.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Tester.API.Controllers;

[ApiController]
[Route("api/answer_options")]
public class AnswerOptionsController : ControllerBase
{
    private readonly IAnswerOptionService _service;
    private readonly IMapper _mapper;

    public AnswerOptionsController(IAnswerOptionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("question/{questionId:int}/correct")]
    public async Task<ActionResult<ApiResponse>> GetCorrectAnswersByQuestion(int questionId)
    {
        var response = new ApiResponse();
        try
        {
            var models = await _service.GetCorrectAnswerOptionsAsync(questionId);
            var dtos = _mapper.Map<List<AnswerOptionResponse>>(models);

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

    [HttpGet("question/{questionId:int}")]
    public async Task<ActionResult<ApiResponse>> GetAllAnswersByQuestion(int questionId)
    {
        var response = new ApiResponse();
        try
        {
            var models = await _service.GetAllAnswerOptionsByQuestionIdAsync(questionId);
            var dtos = _mapper.Map<List<AnswerOptionResponse>>(models);

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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateAnswerOptionRequest dto)
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
            var model = _mapper.Map<AnswerOptionModel>(dto);
            await _service.CreateAnswerOptionAsync(model);

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

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<ApiResponse>> Update ([FromBody] UpdateAnswerOptionRequest dto)
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
            var model = _mapper.Map<AnswerOptionModel>(dto);
            await _service.UpdateAnswerOptionAsync(model);

            response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Delete(int id)
    {
        var response = new ApiResponse();
        try
        {
            await _service.DeleteAnswerOptionAsync(id);
            response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();
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