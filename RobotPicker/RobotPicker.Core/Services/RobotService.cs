using RobotPicker.Core.Models;
using RobotPicker.Core.Repositories;

namespace RobotPicker.Core.Services
{
    public class RobotService : IRobotService
    {
        public readonly IRobotRepository _robotRepository;
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

            var robots = await _robotRepository.GetRobotsAsync();

            if (robots == null)
            {
                return null;
            }

            // order robots by eucledean distance to load using a Min Heap
            var minHeap = new PriorityQueue<RobotLoadInfo, RobotLoadInfo>(Comparer<RobotLoadInfo>.Create((RobotLoadInfo a, RobotLoadInfo b) => Compare(a, b)));

            foreach (var robot in robots) // time complexity O(n)
            {
                minHeap.Enqueue(new RobotLoadInfo(robot, load), new RobotLoadInfo(robot, load)); // time complexity O(log(n))
            }

            var bestRobotToTransportLoad = minHeap.Peek();

            while (minHeap.Count > 0 && minHeap.Peek().DistanceToGoal < 10) // while loop time complexity O(n)
            {
                var robot = minHeap.Dequeue(); // time complexity O(1)
                if (robot.BatteryLevel > bestRobotToTransportLoad.BatteryLevel)
                {
                    bestRobotToTransportLoad = robot;
                }
            }

            return bestRobotToTransportLoad;
        }

        private int Compare(RobotLoadInfo a, RobotLoadInfo b)
        {
            if (a.DistanceToGoal < b.DistanceToGoal)
                return -1;
            if (a.DistanceToGoal > b.DistanceToGoal)
                return 1;
            return 0;
        }
    }
}
