namespace API.Errors;

public class ApiResponse(int statusCode, string? message = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string? Message { get; set; } = message ?? GetDefaultMessageForStatusCode(statusCode);

    private static string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad request",
            401 => "Unauthorized",
            404 => "Not found",
            500 => "Server error",
            _   => null
        };
    }
}