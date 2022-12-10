using System.Text.Json.Serialization;
using CityForum.Shared;

namespace CityForum.WebApi.Models;

public class ErrorResponse
{
    [JsonPropertyName("errorCode")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("errorName")]
    public string ErrorName { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("fieldErrors")]
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }

    public ErrorResponse(Enum resultCode)
    {
        ErrorCode = Convert.ToInt32(resultCode).ToString();
        ErrorName = resultCode.GetDescription();
        ErrorMessage = resultCode.GetEnumDescription();
    }

    public ErrorResponse() { }
}

public class ErrorResponseFieldInfo
{
    [JsonPropertyName("field")]
    public string FieldName { get; set; }

    [JsonPropertyName("errorCode")]
    public string ErrorCode { get; set; }

    [JsonPropertyName("errorName")]
    public string ErrorName { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("details")]
    public object Details { get; set; }
}