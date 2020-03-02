using System.Threading.Tasks;

namespace Kf.Extensions.Microsoft.Hosting.Console
{
    public interface IConsoleApplication
    {
        /// <summary>
        /// Runs the application.
        /// </summary>
        Task Run();
    }
}
