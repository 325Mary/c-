using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class SystemInfoController : ControllerBase
{
    private readonly SystemInfoService _systemInfoService;

    public SystemInfoController(SystemInfoService systemInfoService)
    {
        _systemInfoService = systemInfoService;
    }

    [HttpGet("ram")]
public IActionResult GetRAMInfo()
{
    try
    {
        var availableRAM = _systemInfoService.GetAvailableRAM();
        var totalRAM = _systemInfoService.GetTotalRAM();

        return Ok(new
        {
            AvailableRAM = availableRAM,
            TotalRAM = totalRAM
        });
    }
    catch (PlatformNotSupportedException ex)
    {
        return StatusCode(500, new { Message = "La plataforma no es compatible.", Error = ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = "Ocurrió un error al obtener la información de RAM.", Error = ex.Message });
    }
}

}