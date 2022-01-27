using RobotPicker.Core.Models;

namespace RobotPicker.Core.Utility
{
    public class Calculate
    {
        public static double EucledeanDistance(Robot a, Load l)
        {
            return Math.Sqrt(Math.Pow(a.X - l.X, 2) + Math.Pow(a.Y - l.Y, 2));
        }
    }
}
