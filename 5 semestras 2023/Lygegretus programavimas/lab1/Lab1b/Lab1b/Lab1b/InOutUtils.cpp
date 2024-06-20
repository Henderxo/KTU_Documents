#include "InOutUtils.h"

 vector<Character>  InOutUtils::ReadData(string FileName)
        {
            ifstream dataFile(FileName);
            if (dataFile.fail()) {
                throw invalid_argument("File " + FileName + " not found");
            }

            nlohmann::json data = nlohmann::json::parse(dataFile);

            dataFile.close();

            vector<Character> Characters;

            for (auto& elm : data.items()) {
                nlohmann::json item = elm.value();
                Character Character(item["Name"], item["Age"].get<int64_t>(), item["Power"].get<double>());
                Characters.push_back(Character);
            }
            return Characters;
        }
       void InOutUtils::PrintToFile(string fileName, vector<ProcessedCharacter> Characters, int ageSum, double powerSum, string header)
        {
           int tableWidth = 109;
           ofstream res;
           res.open(fileName);
           res << header << endl;
           res << string(tableWidth, '-') << endl;
           res << "|                              Name |  Age |     Power | Answer                        |" << endl;
           res << string(tableWidth, '-') << endl;
            for (int i = 0; i < Characters.size(); i++)
            {
                res << Characters[i].ToString() << endl;
            }
            res << string(tableWidth, '-') << endl;
            res << "Age sum " << ageSum << endl;
            res << "Power sum " << powerSum << endl;
            res.close();
        }