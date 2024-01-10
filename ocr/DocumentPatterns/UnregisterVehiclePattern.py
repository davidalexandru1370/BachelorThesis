from typing import List, Dict

from DocumentPatterns.DocumentPatternAbstract import DocumentPatternAbstract
from DocumentPatterns.DocumentType import DocumentType
import re


class UnregisterVehiclePattern(DocumentPatternAbstract):

    def __init__(self):
        self.__confidence_levels = {

        }

    def check_for_match(self, word: str) -> float:
        regexes: Dict[str, float] = {
            "certificat(\\s)*de(\\s)*radiere": 100,
            "semnatura(\\s)*vanzatorului": 0.1,
            "dezmembrat": 0.1,
            "exportat": 0.1,
            "furat": 0.2,
            "a(\\s)fost(\\s)radiat": 0.2,
            "semnatura(\\s)si(\\s)stampila": 0.2,
            "inspectoratul(\\s)de(\\s)politie": 0.2,
            "inspectoratul(\\s)de(\\s)polije": 0.2,
            "serviciul(\\s)circulatie": 0.2,
            "Doresc(\\s)sa(\\s)pastrez(\\s)numarul(\\s)de(\\s)inmatriculare": 0.5
        }
        matches: List[float] = []
        for regex in regexes.keys():
            if re.match(regex, word, re.IGNORECASE):
                matches.append(regexes[regex])

        if len(matches) > 0:
            return max(matches)
        return 0.0

    def compute_confidence_level(self, words: List[str]) -> float:
        confidence_level: float = 0.0
        for word, _ in words:
            word = word.lower()
            match: float = self.check_for_match(word)
            if match > 0.0:
                confidence_level += match

        return confidence_level

    def get_document_type(self) -> DocumentType:
        return DocumentType.UnregisterVehicle
