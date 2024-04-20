using System.Text.Json.Serialization;

namespace Domain.Constants;

public enum DocumentType
{
    IdentityCard,
    OwnershipContract,
    UnregisterVehicle,
    NotFound
}