using Microsoft.AspNetCore.Mvc;
using RobotPicker.Core.Models;
using RobotPicker.Core.Services;

namespace RobotPicker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotPickerController : ControllerBase
    {
        private readonly IRobotService _robotService;

        public RobotPickerController(IRobotService robotService) => _robotService = robotService;


        [HttpPost]
        public async Task<IActionResult> Post(Load model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(Load)} can not be null");
            }

            var bestRobotToTrasportLoad = await _robotService.GetBestRobotToTransportLoadAsync(model);
            return Ok(bestRobotToTrasportLoad);
        }
    }
}
