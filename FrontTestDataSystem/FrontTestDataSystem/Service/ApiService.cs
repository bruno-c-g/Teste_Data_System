using FrontTestDataSystem.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontTestDataSystem.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44378/api/DataBase");
        }

        public async Task<bool> CreateAsync(Jobs jobs)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("DataBase", jobs);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao criar os dados: \n\n {response.StatusCode} \n\n {errorContent}");
            }
        }

        public async Task<bool> UpdateAsync(Jobs jobs)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("DataBase", jobs);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao alterar os dados: \n\n {response.StatusCode} \n\n {errorContent}");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"DataBase?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao deletar os dados: \n\n {response.StatusCode} \n\n {errorContent}");
            }
        }

        public async Task<List<Jobs>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"DataBase/all?PageNumber={pageNumber}&PageSize={pageSize}");

            if (!response.IsSuccessStatusCode) return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Jobs>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Jobs>> GetAllAsyncByState(int status, int pageNumber = 1, int pageSize = 10)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"DataBase/by-status?PageNumber={pageNumber}&PageSize={pageSize}&status={status}");

            if (!response.IsSuccessStatusCode) return null;

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Jobs>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
