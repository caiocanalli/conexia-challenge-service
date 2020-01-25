using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Conexia.Challenge.Services.Api.Infrastructure
{
    public class BusService : IHostedService
    {
        readonly IBusControl _busControl;

        public BusService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
