using System.Text.Json.Serialization;

namespace Domain.Constants;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    NotComputed
}