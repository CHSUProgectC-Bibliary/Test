using System.Net.Http.Json;
using BookReviewAPI.Data.Dto;


public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Key");
        // или https, если используете SSL вынести (можно connection string) все в конфиге Key - изменить!!
        
    }

    public  Task<List<BookDto>> GetBooksAsync()
    {
        return _httpClient.GetFromJsonAsync<List<BookDto>>("/Book/All");
    }
    
    // Добавьте другие методы для работы с API
}