using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zadanie9.DTOs;
using Zadanie9.Exceptions;
using Zadanie9.Models;
using Zadanie9.Services;

namespace Zadanie9.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController(IDbService prescriptionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDto prescription)
    {
        try
        {
            return await prescriptionService.AddPrescriptionAsync(prescription);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}