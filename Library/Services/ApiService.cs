using System.Net.Http.Json;
using BookReviewAPI.Data.Dto;


public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5002"); // или https, если используете SSL
    }

    public async Task<List<BookDto>> GetBooksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BookDto>>("/Book/All");
    }

    // Добавьте другие методы для работы с API
}