using RobotPicker.Core.Models;

namespace RobotPicker.Core.Services
{
    public interface IRobotService
    {
        Task<RobotLoadInfo> GetBestRobotToTransportLoadAsync(Load model);
    }
}
