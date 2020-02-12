using SpeedTestApi.Models;
using System.Threading.Tasks;

namespace SpeedTestApi.Services
{
    public interface ISpeedTestEvents
    {
        Task PublishSpeedTest(TestResult SpeedTest);
    }
}
