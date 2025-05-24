using Microsoft.AspNetCore.Mvc;
using Zadanie9.Exceptions;
using Zadanie9.Services;

namespace Zadanie9.Controllers;
[ApiController]
[Route("[controller]")]
public class PatientController(IDbService patientService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var patient = await patientService.GetPatientByIdAsync(id);
            return Ok(patient);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}