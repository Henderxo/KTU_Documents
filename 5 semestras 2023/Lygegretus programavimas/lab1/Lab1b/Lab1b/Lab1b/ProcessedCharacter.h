#pragma once
#include <string>
#include <sstream>
#include <iomanip>
#include "Character.h"
using namespace std;
class ProcessedCharacter : public Character
{
public:

	long computedData;
    ProcessedCharacter();
    ProcessedCharacter(string name, int age, double power);

    ProcessedCharacter(Character character);
    ProcessedCharacter(Character character, long processedData);
	string ToString();
};

