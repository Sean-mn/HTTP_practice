namespace HttpServer;

public class Request
{
    public string Method { get; set; }
    public string Path { get; set; }

    public static Request Parse(string req)
    {
        var lines = req.Split("\r\n");
        var requestLine = lines[0].Split(' ');

        return new Request
        {
            Method = requestLine[0],
            Path = requestLine[1],
        };
    }
}