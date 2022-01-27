# RobotPicker

There are 3 projects in the solution:
1. RobotPicker.API (web API)
2. RobotPicker.Core (core functionality)
3. RobotPicker.Test (Unit tests)

Instructions to test the API:
Using Postman a POST request can be send to http://localhost:5229/RobotPicker. The body of the request should have the following format:
{
  "loadId": 10,
  "x": 41,
  "y": 12
}

In addition to that. The solution also has a unit test project that can be run.

Description of what features, functionality, etc. you would add next and how you would implement them.

In order to determine the best robot to pick the load, I would have taken into consideration other factors such as the terrain and the weather conditions between a robot and the load. A bad weather such as a snow storm or a path with lots of mountains will most likely cause delays for the robot to pick the load as well as lead to higher battery consumption. In both cases, we could probably consider a robot that might be a bit farther away and does not have to travel the same path in order to prevent these issues. I would implement this feature using a weather forecast API and a map API. Once we have the robot route we could check whether there is a bad weather condition along the way or the route is not ideal. If any of those conditions are present then we discard the robot and look for the next robot in the Min Heap that is within the Euclidean distance of the first robot plus x units. X would be an educated guess of up to how many units after the first robot’s distance to the load we would like to explore other robots.
