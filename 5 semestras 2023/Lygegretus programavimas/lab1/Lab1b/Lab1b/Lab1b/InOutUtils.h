#pragma once
#include <string>
#include <iomanip>
#include <list>
#include <fstream>
#include "libraries\nlohmann\json.hpp"
#include "Character.h"
#include <vector>
#include <stdexcept>
#include "ProcessedCharacter.h"
using namespace std;
class InOutUtils
{
public:
	static vector<Character> ReadData(string FileName);
	static void PrintToFile(string fileName, vector<ProcessedCharacter> characters, int ageSum, double powerSum, string header);
};

