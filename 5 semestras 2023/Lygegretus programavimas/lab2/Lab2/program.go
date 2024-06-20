package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"os"
	"sync"
	"time"
)

type Character struct {
	Name  string
	Age   int
	Power float64
}

type ResultCharacter struct {
	Name  string
	Age   int
	Power float64
	Ans   int64
}

func main() {
	// Reading files
	//content, err := ioutil.ReadFile("./IFF-1-9_LiaudanskisN_L1_dat_1.json")
	//content, err := ioutil.ReadFile("./IFF-1-9_LiaudanskisN_L1_dat_2.json")
	content, err := ioutil.ReadFile("./IFF-1-9_LiaudanskisN_L1_dat_3.json")
	if err != nil {
		log.Fatal("Error when opening file: ", err)
	}

	// Now let's unmarshall the data into `dataS`
	var dataS []Character
	err = json.Unmarshal(content, &dataS)
	if err != nil {
		log.Fatal("Error during Unmarshal(): ", err)
	}

	dataRoutineChannelDataIn := make(chan Character, 1) // Will get data in from the dataS into the data.
	dataRoutineChannelDataOut := make(chan Character, 1)

	resultRoutineChannelDataIn := make(chan ResultCharacter, 1)
	resultRoutineChannelDataOut := make(chan ResultCharacter, 1)

	resultRoutineChannelDone := make(chan bool)
	workerRoutinesCount := 10
	// Pagrindine gija palaukia kol paigs sios gijos
	var workersWaitGroup sync.WaitGroup
	for i := 0; i < workerRoutinesCount; i++ {
		workersWaitGroup.Add(1)
		go func() {
			defer workersWaitGroup.Done()
			worker(dataRoutineChannelDataOut, resultRoutineChannelDataIn)
		}()
	}
	go data(dataRoutineChannelDataIn, dataRoutineChannelDataOut, len(dataS)/2)
	go result(resultRoutineChannelDataIn, resultRoutineChannelDataOut, resultRoutineChannelDone)

	// Let's print the unmarshalled data!
	for i := 0; i < len(dataS); i++ {
		dataRoutineChannelDataIn <- dataS[i]
	}
	close(dataRoutineChannelDataIn)
	workersWaitGroup.Wait()
	resultRoutineChannelDone <- true

	resultsFile, err := os.Create("IIFF-1-9_LiaudanskisN_L1_rez.txt")
	check(err)
	resultsFile.WriteString("Result\n" +
		"-----------------------------------------------------------------------------------------------\n" +
		"| Name                                | Age     | Power      | Answer                         |\n" +
		"-----------------------------------------------------------------------------------------------\n")

	for {
		Character, more := <-resultRoutineChannelDataOut
		if more {

			fmt.Fprintf(resultsFile, "|%-37s|%-9d|%-12f|%-d\n", Character.Name, Character.Age, Character.Power, Character.Ans)
		} else {

			break
		}
	}
	resultsFile.WriteString("-----------------------------------------------------------------------------------------------\n")
	defer resultsFile.Close()

	//log.Printf("origin: %s\n", dataS[0].Vardas)
	//log.Printf("user: %d\n", dataS[0].Amzius)
}

func data(dataIn <-chan Character, dataOut chan<- Character, maxSize int) {
	var data []Character
	receivingDone := false
	sendingDone := false

	for {
		if len(data) < maxSize && !receivingDone {
			select {
			case Character, more := <-dataIn:
				if more { // Sees if the chan is still open if there are moe object that will come
					if len(data) < maxSize {
						data = append(data, Character)
					}
				} else {

					receivingDone = true // When all data gets processed we set this to true
				}
			default:
			}
		}
		if len(data) > 0 { // See if we still got more to send away
			dataOut <- data[len(data)-1]
			data = data[:len(data)-1]
		} else if receivingDone {
			sendingDone = true
		}
		if receivingDone && sendingDone {
			break
		}

	}
	close(dataOut) //Close chan dataOut
}

func worker(dataIn <-chan Character, dataOut chan<- ResultCharacter) {
	for {
		Character, more := <-dataIn
		if more {
			processedData := int64(0)
			max := int64(999999999)

			for i := 0; i < Character.Age; i++ {
				for y := 0.0; y < Character.Power; y += 0.1 {
					processedData += int64(Character.Name[int(math.Floor(float64(i)*y))%len(Character.Name)]) * 2
				}
			}

			processedData = max - processedData
			time.Sleep(500 * time.Millisecond)
			if Character.Power < 100 {
				dataOut <- ResultCharacter{
					Name:  Character.Name,
					Age:   Character.Age,
					Power: Character.Power,
					Ans:   processedData,
				}
			}
		} else {
			//fmt.Println("workerRoutine received all jobs")
			return
		}
	}
}

func result(dataIn <-chan ResultCharacter, dataOut chan<- ResultCharacter, done <-chan bool) {
	var data []ResultCharacter
	receivingDone := false
	for {
		select {
		case <-done: // When done chan gets signal we done
			receivingDone = true
		case Character := <-dataIn:
			//fmt.Println("result received", Character)
			data = append(data, Character)
		}
		if receivingDone {
			break
		}
	}
	for i := 0; i < len(data); i++ {
		dataOut <- data[i]
	}
	close(dataOut)
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}
