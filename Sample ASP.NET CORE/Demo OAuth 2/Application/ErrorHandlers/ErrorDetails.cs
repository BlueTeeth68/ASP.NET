using System.Net;
using System.Text.Json;

namespace Application.ErrorHandlers;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}