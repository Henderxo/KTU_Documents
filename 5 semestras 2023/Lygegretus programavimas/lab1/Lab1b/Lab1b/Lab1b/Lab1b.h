#pragma once
#include <iostream>
#include <string>
#include "ProcessedCharacter.h"
#include "InOutUtils.h"
#include <list>
#include <vector>
#include <omp.h>
#include <math.h>
class Program
{
public:

    static vector<Character> characters;
    static vector<ProcessedCharacter> processedCharacters;
    static int main();
    
};