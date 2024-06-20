#pragma once

#include <string>
using namespace std;


class Character {
public:
    string Name;
    int Age;
    double Power;
    Character();
	Character(string name, int age, double power);
};