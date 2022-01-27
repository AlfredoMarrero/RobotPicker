using RobotPicker.Core.Models;

namespace RobotPicker.Core.Repositories
{
    public interface IRobotRepository
    {
        Task<IEnumerable<Robot>> GetRobotsAsync();
    }
}
