using System.Threading.Tasks;
using WebApplication.Definitions;
using WebApplication.Processing;

namespace WebApplication.Job
{
    public abstract class JobItemBaseBaseBase
    {

        public override async Task ProcessJobAsync(IResultSender resultSender, string message)
        {
            using System.IDisposable scope = Logger.BeginScope("Update Model ({Id})");

            Logger.LogInformation($"ProcessJob (Update) {Id} for project {ProjectId} started.");

            (ProjectStateDTO state, FdaStatsDTO stats, string reportUrl) = await ProjectWork.DoSmartUpdateAsync(Parameters, ProjectId);

            Logger.LogInformation(message: $"ProcessJob (Update) {Id} for project {ProjectId} completed.");

            // send that we are done to client
            await resultSender.SendSuccessAsync(state, stats, reportUrl);
        }
    }
}