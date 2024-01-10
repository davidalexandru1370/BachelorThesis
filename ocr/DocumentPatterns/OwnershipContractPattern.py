from typing import List, Dict

from DocumentPatterns.DocumentPatternAbstract import DocumentPatternAbstract
from DocumentPatterns.DocumentType import DocumentType
import re


class OwnershipContractPattern(DocumentPatternAbstract):

    def __init__(self):
        self.__confidence_levels = {
            "contract devanzare-cumparare": 0.75,
            "contract de vanzare-cumparare": 0.75,
            "contract de vanzare cumparare": 0.75,
            "1vanzator": 0.25,
            "vanzator": 0.25,
            "2cumparator": 0.25,
            "cumparator": 0.25,
            "3obiectul contractului": 0.25,
            "obiectul contractului": 0.25,
            "obiectulcontractului": 0.25,
            "3obiectulcontractului": 0.25,
            "4pretul": 0.25,
            "cilindree": 0.1,
            "persoana fizica": 0.1,
            "persoana juridica": 0.1,
            "persoanafizica": 0.1,
            "persoanajuridica": 0.1,
            "pentru un vehicul folosit": 0.2,

        }

    def check_for_match(self, word: str) -> float:
        regexes: Dict[str, float] = {
            "contract(\\s)*de(\\s)*vanzare.{0,2}cumparare": 100,
            "pentru(\\s)*un(\\s)*vehicul(\\s)*folosit": 0.25,
            "registrul comertului": 0.1,
            "persoana(\\s)*fizica": 0.1,
            "persoana(\\s)*juridica": 0.1,
            "cumparator": 0.1,
            "vanzator": 0.1,
            "vehicul": 0.1,
            "obiectul(\\s)*contractului": 0.1,
            "inspectia(\\s)*tehnica(\\s)*periodica": 0.1,
            "pretul": 0.1,
            "Locul(\\s)*incheierii(\\s)*contractului": 0.1,
            "Semnatura(\\s)*cumparatorului": 0.1,
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
            elif word in self.__confidence_levels:
                confidence_level += self.__confidence_levels[word]

        return confidence_level

    def get_document_type(self) -> DocumentType:
        return DocumentType.OwnershipContract
