using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using SpeedTestApi.Models;

namespace SpeedTestApi.Services
{
    public class SpeedTestEvents : ISpeedTestEvents, IDisposable
    {
        private readonly EventHubClient _client;
        private readonly string _connectionString;

        public SpeedTestEvents(string connectionString, string entityPath)
        {
            _connectionString = connectionString;
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(_connectionString)
            {
                EntityPath = entityPath
            };

            _client = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }

        public async Task PublishSpeedTest(TestResult speedTest)
        {
            var message = JsonSerializer.Serialize(speedTest);
            var data = new EventData(Encoding.UTF8.GetBytes(message));

            await _client.SendAsync(data);
        }

        public void Dispose()
        {
            _client.CloseAsync();
        }
    }
}
