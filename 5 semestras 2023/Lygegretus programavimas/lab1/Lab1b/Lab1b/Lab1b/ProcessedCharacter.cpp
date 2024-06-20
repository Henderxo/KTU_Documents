#include "ProcessedCharacter.h"



ProcessedCharacter::ProcessedCharacter() {

    }
ProcessedCharacter::ProcessedCharacter(string name, int age, double power) : Character(name, age, power)
    {
    }

ProcessedCharacter::ProcessedCharacter(Character character) : Character(character.Name, character.Age, character.Power)
    {
    }
ProcessedCharacter::ProcessedCharacter(Character character, long processedData) : ProcessedCharacter(character)
    {
        computedData = processedData;
    }
string ProcessedCharacter::ToString()
    {
        stringstream stream;
        stream << "| " 
            << setfill(' ') 
            << setw(35) << Name << " | " 
            << setw(7) << Age << " | " 
            << setw(10) << Power << " | " 
            << setw(20) << computedData;
        return stream.str();
    }