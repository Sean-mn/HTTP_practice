using System.Text;

namespace HttpServer;

public enum StatusCode
{
    Ok = 200,
    NotFound = 404,
}

public class Response
{
    public string HttpVersion { get; } = "HTTP/1.1";
    
    private StatusCode _statusCode;

    public StatusCode StatusCode
    {
        get => _statusCode;
        set
        {
            _statusCode = value;
            StatusMessage = value switch
            {
                StatusCode.Ok => "OK",
                StatusCode.NotFound => "Not Found",
                _ => "Unknown"
            };
        }
    }
    public string StatusMessage { get; private set; } = "OK";
    
    public string ContentType { get; set; } = "text/html";
    public string Body { get; set; } = string.Empty;
    
    public Dictionary<string, string> Headers { get; } = new();

    public Response()
    {
    }
    public Response(string content)
    {
        Body = content;
    }

    public string Build()
    {
        StringBuilder sb = new();
        sb.AppendLine($"{HttpVersion} {StatusCode} {StatusMessage}");
        sb.AppendLine($"Content-Type: {ContentType}");
        sb.AppendLine($"Content-Length: {Encoding.UTF8.GetByteCount(Body)}");

        foreach (var header in Headers)
        {
            sb.AppendLine($"{header.Key}: {header.Value}");
        }

        sb.AppendLine();
        sb.Append(Body);
        
        return sb.ToString();
    }
}