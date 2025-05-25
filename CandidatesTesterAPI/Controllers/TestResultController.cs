using System.Net;
using AutoMapper;
using CandidatesTesterAPI.DTOs.TestResultDTOs;
using CandidatesTesterAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;

namespace CandidatesTesterAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestResultsController : ControllerBase
{
    private readonly ITestResultService _service;
    private readonly IMapper _mapper;

    public TestResultsController(ITestResultService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse>> GetById(int id)
    {
        var response = new ApiResponse();
        try
        {
            var model = await _service.GetResultByIdAsync(id);
            if (model == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMessages.Add("Test result not found");
                return NotFound(response);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.Result = _mapper.Map<TestResultResponse>(model);
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

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<ApiResponse>> GetByUser(int userId)
    {
        var response = new ApiResponse();
        try
        {
            var models = await _service.GetResultsByUserAsync(userId);
            var dtos = _mapper.Map<IEnumerable<TestResultResponse>>(models);

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

    [HttpGet("user/{userId:int}/average")]
    public async Task<ActionResult<ApiResponse>> GetAverage(int userId)
    {
        var response = new ApiResponse();
        try
        {
            var avg = await _service.GetAverageScoreAsync(userId);
            response.StatusCode = HttpStatusCode.OK;
            response.Result = new { AveragePercent = avg };
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
    public async Task<ActionResult<ApiResponse>> Create([FromBody] CreateTestResultRequest dto)
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
            var model = _mapper.Map<TestResultModel>(dto);
            model.CompletedAt = DateTime.UtcNow;

            await _service.SaveTestResultAsync(model);

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
}
