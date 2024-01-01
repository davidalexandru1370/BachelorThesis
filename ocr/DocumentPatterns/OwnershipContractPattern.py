from typing import List

from DocumentPatterns.DocumentPatternAbstract import DocumentPatternAbstract
from DocumentPatterns.DocumentType import DocumentType


class OwnershipContractPattern(DocumentPatternAbstract):
    def compute_confidence_level(self, words: List[str]) -> float:
        return 0.0

    def get_document_type(self) -> DocumentType:
        return DocumentType.OwnershipContract
