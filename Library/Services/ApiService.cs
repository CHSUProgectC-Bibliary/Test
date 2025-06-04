using System.Net.Http.Json;


public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserDto?> LoginAsync(LoginDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/account/login", dto);
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            return user;
        }

        return null;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var userDto = new CreateUserDto(
            User_name: dto.UserName,
            Email: dto.Email,
            Password: dto.Password
        );

        var response = await _httpClient.PostAsJsonAsync("User", userDto);
        return response.IsSuccessStatusCode;
    }



    public Task<List<BookDto>> GetBooksAsync()
    {
        return _httpClient.GetFromJsonAsync<List<BookDto>>("Book/All");
    }

    public async Task<List<BookDto>> GetBooksByCategoryAsync(string section)
    {
        try
        {
            string encodedSection = System.Net.WebUtility.UrlEncode(section);
            string url = $"Book/ByCategory/{encodedSection}";

            Console.WriteLine($"Requesting books for category: {section} (encoded: {encodedSection})");

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return new List<BookDto>();
            }

            var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
            Console.WriteLine($"Received {books?.Count ?? 0} books for category {section}");
            return books ?? new List<BookDto>();
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Exception while getting books: {ex}");
            return new List<BookDto>();
        }
    }


    // Добавьте другие методы для работы с API
}