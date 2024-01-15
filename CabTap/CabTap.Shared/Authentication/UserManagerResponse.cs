using System.Net;

namespace CabTap.Shared.Authentication;

public class UserManagerResponse
{
    public string Message { get; init; } = null!;
    public bool IsSuccessful { get; init; }
    public IEnumerable<string?>? Errors { get; set; }
    public string Token { get; init; } = null!;
    public DateTime? ExpireDate { get; set; }
    public HttpStatusCode StatusCode { get; init; }
}