using System.Net;
using AutoMapper;
using Tester.Shared.DTOs.QuestionDTOs;
using Tester.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Tester.API.Controllers;

[ApiController]
[Route("api/questions")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;
    private readonly IMapper _mapper;

    public QuestionsController(IQuestionService questionService, IMapper mapper)
    {
        _questionService = questionService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test/{testId:int}")]
    public async Task<ActionResult<ApiResponse>> GetByTest(int testId)
    {
        var response = new ApiResponse();
        try
        {
            var models = await _questionService.GetQuestionsByTestIdAsync(testId);
            var dtos = _mapper.Map<List<QuestionResponse>>(models);

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
    public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateQuestionRequest dto)
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
            var model = _mapper.Map<QuestionModel>(dto);
            await _questionService.CreateQuestionAsync(model);

            response.StatusCode = HttpStatusCode.Created;
            return StatusCode((int)response.StatusCode, response);
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
    [HttpPut("update")]
    public async Task<ActionResult<ApiResponse>> Update([FromBody] UpdateQuestionRequest dto)
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
            var model = _mapper.Map<QuestionModel>(dto);
            await _questionService.ChangeQuestionTypeAsync(model);

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
