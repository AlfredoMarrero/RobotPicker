using RobotPicker.Core.Models;

namespace RobotPicker.Core.Repositories
{
    public class RobotRepository : IRobotRepository
    {
        private const string RobotsUrl = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";
        private readonly IHttpClientFactory _httpClientFactory;

        public RobotRepository(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<Robot>> GetRobotsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(RobotsUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Robot>>();
            }

            return null;
        }
    }
}
