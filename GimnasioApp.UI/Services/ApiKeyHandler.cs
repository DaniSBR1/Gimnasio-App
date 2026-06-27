namespace GimnasioApp.UI.Services
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public ApiKeyHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = _configuration["ApiSettings:ApiKey"];
            request.Headers.Add("X-Api-Key", apiKey);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
