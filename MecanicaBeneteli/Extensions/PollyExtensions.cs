using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace MecanicaBeneteli.WebApp.Extensions
{
    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentarNovamente(ILoggerFactory loggerFactory)
        {
            var retry = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    TimeSpan.FromSeconds(6),
                }, (outcome, timespan, retryCount, context) =>
                {
                    ILogger<PollyExtensions> modelLogger = loggerFactory.CreateLogger<PollyExtensions>();

                    modelLogger.LogError(
                                            $"Reenviando requisição para API. Tentativa {retryCount} \n\n" +
                                            $"Uri: {outcome.Result?.RequestMessage.RequestUri.AbsoluteUri} \n" +
                                            $"Method: {outcome.Result?.RequestMessage.Method.Method} \n" +
                                            $"Motivo do Reenvio: \n {outcome.Result?.ToString()}"
                                        );
                });

            return retry;
        }
    }
}
