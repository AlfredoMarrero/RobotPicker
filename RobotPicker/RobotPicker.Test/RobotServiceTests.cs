using Moq;
using RobotPicker.Core.Models;
using RobotPicker.Core.Repositories;
using RobotPicker.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RobotPicker.Test
{
    public class RobotServiceTests
    {
        private readonly Mock<IRobotRepository> _robotRepositoryMock;
        private readonly IRobotService _sut;

        public RobotServiceTests()
        {
            _robotRepositoryMock = new Mock<IRobotRepository>();
            _sut = new RobotService(_robotRepositoryMock.Object);
        }


        [Fact]
        public async Task GetBestRobotToTransportLoadAsync_ShouldThrowException_WhenLoadIsNull()
        {
            //act & assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetBestRobotToTransportLoadAsync(null));
        }

        [Fact]
        public async Task GetBestRobotToTransportLoadAsync_ShouldReturnClosesRobot_WhenRobotsAreMoreThan10UnitsAwayFromLoad()
        {
            //arrange
            var load = new Load()
            {
                LoadId = 100,
                X = 0,
                Y = 0
            };

            List<Robot> robots = new List<Robot>()
            {
                new Robot() { RobotId = 4, BatteryLevel = 10, X = 100, Y = 200 },
                new Robot() { RobotId = 5, BatteryLevel = 10, X = 200, Y = 400 },
                new Robot() { RobotId = 6, BatteryLevel = 10, X = 400, Y = 800 }
            };

            _robotRepositoryMock.Setup(x => x.GetRobotsAsync()).ReturnsAsync(robots);

            //act
            var result = await _sut.GetBestRobotToTransportLoadAsync(load);

            //assert
            result.RobotId.Equals(robots[0].RobotId);
        }

        [Fact]
        public async Task GetBestRobotToTransportLoadAsync_ShouldReturnRobotWithMostBattery_WhenMoreThanOneRobotIs10UnitsAwayFromLoad()
        {
            //arrange
            var load = new Load()
            {
                LoadId = 100,
                X = 0,
                Y = 0
            };

            List<Robot> robots = new List<Robot>()
            {
                new Robot() { RobotId = 4, BatteryLevel = 10, X = 1, Y = 2 },
                new Robot() { RobotId = 5, BatteryLevel = 5, X = 2, Y = 4 },
                new Robot() { RobotId = 6, BatteryLevel = 2, X = 4, Y = 5 }
            };

            _robotRepositoryMock.Setup(x => x.GetRobotsAsync()).ReturnsAsync(robots);

            //act
            var result = await _sut.GetBestRobotToTransportLoadAsync(load);

            //assert
            result.RobotId.Equals(robots[0].RobotId);
        }
    }
}
