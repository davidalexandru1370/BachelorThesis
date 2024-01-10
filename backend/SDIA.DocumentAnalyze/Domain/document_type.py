from enum import Enum


class DocumentType(Enum):
    IdentityCard = 0,
    OwnershipContract = 1,
    UnregisterVehicle = 2,
    NotFound = 999

    def serialize(self):
        return self.value