using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;

public static class PlaylistAPIService
{
    public static string? PlaylistId { get; private set; }

    private const int MaxElementsInPlaylist = 200;
    private const string APIKeyFileName = "user_secrets.txt";

    public static void TrySetPlaylistId(string url)
    {
        if (string.IsNullOrEmpty(url))
            throw new Exception("You are trying to set url, but it is null or empty");

        PlaylistId = url;
    }

    public static async Task<PlaylistItemListResponse> RequestPlaylistData(CancellationToken token)
    {
        if (string.IsNullOrEmpty(PlaylistId))
            throw new ArgumentNullException("Tried to send a request with empty URL in PlaylistAPIService");
        
        var request = CreateRequest();
        Console.WriteLine("Request was created");

        return await request.ExecuteAsync(token);
    }

    private static PlaylistItemsResource.ListRequest CreateRequest()
    {
        var initializer = new BaseClientService.Initializer() { ApiKey = ReadApiKeyFromSuperSecretFile() };

        if (initializer.ApiKey is null)
            throw new ArgumentNullException("The API key wasn't assigned");

        var youtubeService = new YouTubeService(initializer);
        var request = youtubeService.PlaylistItems.List("snippet,contentDetails");

        request.MaxResults = MaxElementsInPlaylist;
        request.PlaylistId = PlaylistId;
        return request;
    }

    private static string? ReadApiKeyFromSuperSecretFile()
    {
        try
        {
            string apiKeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, APIKeyFileName);

            if (!File.Exists(apiKeyFilePath))
                throw new FileNotFoundException($"File not found at path: {apiKeyFilePath}");

            return File.ReadAllText(apiKeyFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return null;
        }
    }
}