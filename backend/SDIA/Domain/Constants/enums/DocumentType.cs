using System.Text.Json.Serialization;

namespace Domain.Constants;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    IdentityCard = 0,
    OwnershipContract = 1,
    UnregisterVehicle = 2,
    NotFound = 999
}