namespace LINQPadQuery;

public static class FileSystemUtil
{
    public static async Task WriteToFileAsync(FileInfo fileInfo, ReadOnlyMemory<byte> content)
    {
        await using FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, BufferSize, true);
        await fileStream.WriteAsync(content);
    }
    
    public static async Task<Memory<byte>> ReadFromFileAsync(FileInfo fileInfo)
    {
        await using FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, true);
        using var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    private const int BufferSize = 1024;
}