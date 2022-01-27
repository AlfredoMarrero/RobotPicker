using RobotPicker.Core.Models;
using RobotPicker.Core.Repositories;

namespace RobotPicker.Core.Services
{
    public class RobotService : IRobotService
    {
        public readonly IRobotRepository _robotRepository;
        public const int UnitsToCheckBatteryLevel = 10;
        public RobotService(IRobotRepository robotRepository) => _robotRepository = robotRepository;
        

        /// <summary>
        /// GetBestRobotToTransportLoadAsync - time complexity O(n), space complexity O(n)
        /// </summary>
        /// <param name="load"></param>
        /// <returns></returns>
        public async Task<RobotLoadInfo> GetBestRobotToTransportLoadAsync(Load load)
        {
            if (load == null)
            {
                throw new ArgumentNullException(nameof(load));
            }

            var robots = await _robotRepository.GetRobotsAsync(); // space complexity O(n)

            if (robots == null)
            {
                return null;
            }

            RobotLoadInfo bestRobotByDistance = new RobotLoadInfo(robots.ToArray()[0], load);
            RobotLoadInfo bestRobotByBatteryLevel = null;
            foreach (var robot in robots) // time complexity O(n)
            {
                var robotInfo = new RobotLoadInfo(robot, load);
                if (bestRobotByBatteryLevel == null && robotInfo.DistanceToGoal < bestRobotByDistance.DistanceToGoal)
                {
                    bestRobotByDistance = robotInfo;
                }

                if(robotInfo.DistanceToGoal < UnitsToCheckBatteryLevel && 
                    (bestRobotByBatteryLevel == null || robotInfo.BatteryLevel > bestRobotByBatteryLevel.BatteryLevel))
                {
                    bestRobotByBatteryLevel = robotInfo;
                }
            }

            return bestRobotByBatteryLevel ?? bestRobotByDistance;
        }
    }
}
