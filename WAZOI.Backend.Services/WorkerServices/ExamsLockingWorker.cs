using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WAZOI.Backend.Services.Common;

namespace WAZOI.Backend.Services;

public class ExamsLockingWorker : BackgroundService
{
    #region Fields

    private readonly IServiceScopeFactory _scopeFactory;

    #endregion Fields

    #region Constructors

    public ExamsLockingWorker(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    #endregion Constructors

    #region Methods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(30000);

            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                try
                {
                    var _examService = scope.ServiceProvider.GetRequiredService<IExamService>();
                    var exams = await _examService.GetExamsForLockingAsync();

                    foreach (var exam in exams)
                    {
                        exam.IsLocked = true;
                        await _examService.UpdateAsync(exam);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }

    #endregion Methods
}