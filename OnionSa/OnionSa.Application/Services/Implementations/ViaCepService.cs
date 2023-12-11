using OnionSa.Application.Services.Interfaces;
using System.Text.Json;

namespace OnionSa.Application.Services.Implementations
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        public ViaCepService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<ViaCepResponse> GetLocationByCepAsync(string cep)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{cep}/json/");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponse>(responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return viaCepResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null; // Trate qualquer exceção ou erro de conexão
            }
        }
    }
}
