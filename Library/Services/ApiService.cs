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
    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/account/register", dto);
        return response.IsSuccessStatusCode;
    }

    public Task<List<BookDto>> GetBooksAsync()
    {
        return _httpClient.GetFromJsonAsync<List<BookDto>>("/Book/All");
    }

    public async Task<List<BookDto>> GetBooksByCategoryAsync(string Section)
    {
        string encodedSection = System.Net.WebUtility.UrlEncode(Section);
        string url = $"/Book/ByCategory/{encodedSection}";

        Console.WriteLine($"Запрос к API: {url}");

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Ошибка запроса: {response.StatusCode}");
            return null;
        }

        var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
        Console.WriteLine($"Получено книг: {books?.Count ?? 0}");
        return books;
    }


    // Добавьте другие методы для работы с API
}