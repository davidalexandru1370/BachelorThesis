using System.Text.Json.Serialization;

namespace Domain.Constants.Enums;

public enum DocumentType
{
    IdentityCard,
    OwnershipContract,
    UnregisterVehicle,
    NotFound
}