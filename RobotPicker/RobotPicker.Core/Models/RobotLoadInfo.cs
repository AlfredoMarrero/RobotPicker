using RobotPicker.Core.Utility;

namespace RobotPicker.Core.Models
{
    public class RobotLoadInfo
    {
        public RobotLoadInfo(Robot r, Load load)
        {
            RobotId = r.RobotId;
            DistanceToGoal = Calculate.EucledeanDistance(r, load);
            BatteryLevel = r.BatteryLevel;
        }

        public int RobotId { get; set; }
        public double DistanceToGoal { get; set; }
        public int BatteryLevel { get; set; }
    }
}
