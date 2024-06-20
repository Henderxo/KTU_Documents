#include "Lab1b.h"


vector<Character> Program::characters;
vector<ProcessedCharacter> Program::processedCharacters;
int Program::main()
    {
        string FileName = "IFF-1-9_LiaudanskisN_L1_dat_1.json";
        //string FileName = "IFF-1-9_LiaudanskisN_L1_dat_2.json";
        //string FileName = "IFF-1-9_LiaudanskisN_L1_dat_3.json";

        string resFilename = "IFF-1-9_LiaudanskisN_L1_rez.txt";
        characters = InOutUtils::ReadData(FileName);


        int ageSum = 0;
        double powerSum = 0;
        
        int totalThreads = 5;

        // size = 27, threads = 6
        int minElementsPerThread = characters.size() / totalThreads;   // 4
        int threadsWithExtraElement = characters.size() % totalThreads;// 3

        omp_set_num_threads(totalThreads);
        #pragma omp parallel reduction(+:ageSum) reduction(+:powerSum)
        {
            int threadIndex = omp_get_thread_num();

            int totalElements = minElementsPerThread;
            if (threadIndex < threadsWithExtraElement) {
                totalElements++;
            }

            for (int i = 0; i < totalElements; i++) {
                int indexInVector = totalThreads * i + threadIndex;
                Character character = characters.at(indexInVector);
                long processedData = 0;
                long max = 999999999;
                for (int i = 0; i < character.Age; i++)
                {
                    for (double y = 0; y < character.Power; y += 0.1)
                    {
                        processedData += (int)character.Name[(int)floor(i * y) % character.Name.length()] * 2;
                    }
                }

                processedData = max - processedData;
                ProcessedCharacter processedCharacter = ProcessedCharacter(character, processedData);
                if (processedCharacter.Power < 100)
                {
                    ageSum += processedCharacter.Age;
                    powerSum += processedCharacter.Power;

                    #pragma omp critical
                    {
                        int position = 0;
                        while (position < processedCharacters.size() && processedCharacters[position].Name.compare(processedCharacter.Name) < 0)
                        {
                            position++;
                        }
                        processedCharacters.insert(processedCharacters.begin() + position, processedCharacter);
                    }
                }
            }

        }
        InOutUtils::PrintToFile(resFilename, processedCharacters, ageSum, powerSum, "Results");
        return 0;
    }

int main() {
    Program program;
    return program.main();
}