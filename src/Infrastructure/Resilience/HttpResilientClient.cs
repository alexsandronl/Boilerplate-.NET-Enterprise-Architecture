using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Resilience
{
    /// <summary>
    /// Cliente HTTP resiliente usando Polly (retry + circuit breaker).
    /// </summary>
    public class HttpResilientClient
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy;

        public HttpResilientClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _retryPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    (result, time, retryCount, context) =>
                    {
                        // Log de tentativa de retry
                    });

            _circuitBreakerPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Executa uma requisição GET resiliente.
        /// </summary>
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _retryPolicy.WrapAsync(_circuitBreakerPolicy)
                .ExecuteAsync(() => _httpClient.GetAsync(url));
        }
    }
} 