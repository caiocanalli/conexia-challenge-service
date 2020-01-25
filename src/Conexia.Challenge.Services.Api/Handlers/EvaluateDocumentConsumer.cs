using Conexia.Challenge.Application.Documents.Services.Interfaces;
using Conexia.Challenge.Infra.Framework.Contracts;
using Conexia.Challenge.Infra.IoC;
using MassTransit;
using SimpleInjector.Lifestyles;
using System.Threading.Tasks;

namespace Conexia.Challenge.Services.Api.Handlers
{
    public class EvaluateDocumentConsumer
        : IConsumer<EvaluateDocumentEvent>
    {
        public async Task Consume(ConsumeContext<EvaluateDocumentEvent> context)
        {
            var container = IoC.RecoverContainer();

            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var documentAppService = container.GetInstance<IDocumentAppService>();

                await documentAppService.EvaluateAsync(context.Message.Id);
            }
        }
    }
}
