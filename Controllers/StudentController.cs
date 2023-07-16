using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wsei.Jwt.DTOs;
using Wsei.Jwt.Services;

namespace Wsei.Jwt.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger, IStudentService studentService)
    {
        _logger = logger;
        _studentService = studentService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _studentService.ListAsync());
    }

    [HttpGet]
    [Route("{studentId:int}")]
    public async Task<IActionResult> GetById(int studentId)
    {
        var result = await _studentService.GetAsync(studentId);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    [Authorize(Policy = "Bearer")]
    public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
    {
        if (string.IsNullOrEmpty(dto.LastName))
            return BadRequest("Last name cannot be empty");
        if (string.IsNullOrEmpty(dto.Name))
            return BadRequest("Name cannot be empty");
        if (dto.Age is < 0 or > 100)
            return BadRequest("Age must be in range 0-100");

        await _studentService.CreateAsync(dto);

        return Created("", "null");
    }
}
