import re
from typing import List, Dict

from DocumentPatterns.document_pattern_abstract import DocumentPatternAbstract


class IdentityCardPattern(DocumentPatternAbstract):
    def __init__(self):
        self.__confidence_levels = {
            "roumaine": 0.25,
            "romania": 0.25,
            "românia": 0.2,
            "d'identite": 0.25,
            "d'identité": 0.25,
            "d'identiti": 0.25,
            "d'identit": 0.25,
            "carte": 0.1,
            "identitate": 0.25,
            "carte de identitate": 0.4,
            "cnp": 0.1,
            "nume": 0.1,
            "nom": 0.1,
            "lastname": 0.1,
            "spclep": 0.25,
            "prenume": 0.1,
        }

    def check_for_match(self, word: str) -> float:
        """Match a word with the pattern."""
        regexes: Dict[str, float] = {"cnp[0-9]{5,}": 0.25,
                                     "^spclep.*": 0.25,
                                     "jud[.].*": 0.1,
                                     "seria.*[0-9]{4,}": 0.25,
                                     "idroue[a-z]+[<]+": 0.5
                                     }
        for regex in regexes.keys():
            if re.match(regex, word, re.IGNORECASE):
                return regexes[regex]
        return 0.0

    def compute_confidence_level(self, words: List[str]) -> float:
        """Create a document from the pattern."""
        confidence_level: float = 0.0
        for word in words:
            word = word.lower()
            match: float = self.check_for_match(word)
            if match > 0.0:
                confidence_level += match
            elif word in self.__confidence_levels:
                confidence_level += self.__confidence_levels[word]

        return confidence_level
