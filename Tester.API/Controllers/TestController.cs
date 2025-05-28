using AutoMapper;
using Tester.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tester.Shared.DTOs.TestDTOs;
using Tester.Core.Models;
using Tester.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Tester.API.Controllers;

[ApiController]
[Route("api/tests")]
public class TestsController : ControllerBase
{
    private readonly ITestService _testService;
    private readonly IMapper _mapper;

    public TestsController(ITestService testService, IMapper mapper)
    {
        _testService = testService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetAll()
    {
        var response = new ApiResponse();
        try
        {
            var models = await _testService.GetAllTestsAsync();
            var dtos = _mapper.Map<IEnumerable<TestResponse>>(models);

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse>> GetById(int id)
    {
        var response = new ApiResponse();
        try
        {
            var model = await _testService.GetTestByIdAsync(id);
            if (model == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.ErrorMessages.Add("Test not found");
                return NotFound(response);
            }

            response.StatusCode = HttpStatusCode.OK;
            response.Result = _mapper.Map<TestResponse>(model);
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
    public async Task<ActionResult<ApiResponse>> Create([FromBody] TestRequest dto)
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
            var model = _mapper.Map<TestModel>(dto);
            await _testService.CreateTestAsync(model);

            response.StatusCode = HttpStatusCode.Created;
            response.Result = null;
            return CreatedAtAction(nameof(GetById),
                                   new { id = model.TestId },
                                   response);
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
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] TestRequest dto)
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
            var model = _mapper.Map<TestModel>(dto);
            model.TestId = id;

            await _testService.UpdateTestAsync(model);

            response.StatusCode = HttpStatusCode.NoContent;
            response.Result = null;
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
            await _testService.DeleteTestAsync(id);
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

