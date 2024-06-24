namespace LINQPadQuery;

/// <summary>
/// A utility class for working with URIs.
/// </summary>
public static class UriUtil
{
    public static async Task<ReadOnlyMemory<byte>> DownloadAsync(Uri uri)
    {
        using var client = new HttpClient();
        return await client.GetByteArrayAsync(uri);
    }
    
    public static async Task<string> DownloadStringAsync(Uri uri)
    {
        using var client = new HttpClient();
        return await client.GetStringAsync(uri);
    }
    
    public static async Task<Stream> DownloadStreamAsync(Uri uri)
    {
        using var client = new HttpClient();
        return await client.GetStreamAsync(uri);
    }
    
    public static async Task<HttpResponseMessage> DownloadResponseAsync(Uri uri)
    {
        using var client = new HttpClient();
        return await client.GetAsync(uri);
    }
}