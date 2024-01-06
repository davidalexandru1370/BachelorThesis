from abc import abstractmethod, ABC
from typing import List

from Domain.document_type import DocumentType


class DocumentPatternAbstract(ABC):
    """Abstract class for document patterns."""

    def __init__(self):
        """Initialize the document pattern."""
        pass

    @abstractmethod
    def compute_confidence_level(self, words: List[str]) -> float:
        pass

    @abstractmethod
    def get_document_type(self) -> DocumentType:
        pass
