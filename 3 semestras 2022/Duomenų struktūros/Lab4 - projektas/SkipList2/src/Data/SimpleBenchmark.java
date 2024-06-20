package Data;
import utils.*;
import java.util.Arrays;
import java.util.Collections;
import java.util.Locale;
import java.util.Random;
import java.io.*;

public class SimpleBenchmark {

    Car[] cars;
    LinkedList<Car> carLinkedList;
    SkipList<Car> carSkipList;
    Random rg = new Random();
    int[] counts = {5, 100, 200, 300, 400, 500, 600, 700, 800};
    void generateCars(int count) {
        String[][] makesAndModels = { // galimų automobilių markių ir jų modelių masyvas
                {"Mazda", "121", "323", "626", "MX6"},
                {"Ford", "Fiesta", "Escort", "Focus", "Sierra", "Mondeo"},
                {"Saab", "92", "96"},
                {"Honda", "Accord", "Civic", "Jazz"},
                {"Renault", "Laguna", "Megane", "Twingo", "Scenic"},
                {"Peugeot", "206", "207", "307"}
        };
        cars = new Car[count];
        rg.setSeed(2017);
        for (int i = 0; i < count; i++) {
            int makeIndex = rg.nextInt(makesAndModels.length);        // markės indeksas  0..
            int modelIndex = rg.nextInt(makesAndModels[makeIndex].length - 1) + 1;// modelio indeksas 1..
            cars[i] = new Car((makesAndModels[makeIndex][0] + makesAndModels[makeIndex][modelIndex] + Integer.toString(1994 + rg.nextInt(20)) + Integer.toString(6000 + rg.nextInt(222_000)) + Double.toString(1000 + rg.nextDouble() * 350_000)));
        }
        Collections.shuffle(Arrays.asList(cars));
        carLinkedList = new LinkedList<>();
        carSkipList = new SkipList<>(0.5);
    }
    void generateAndExecute(int elementCount) {
        generateCars(elementCount);
        double t0 = System.nanoTime();
        for(Car car : cars)
        {
            carLinkedList.insert(car);
        }

        double t1 = System.nanoTime();
        for(Car car : cars)
        {
            carLinkedList.search(car);
        }
        double t2 = System.nanoTime();
        for(Car car : cars)
        {
            carLinkedList.delete(car);
        }
        double t3 = System.nanoTime();
        //----------------------------
        for(Car car : cars)
        {
            carSkipList.insert(car);
        }


        double t4 = System.nanoTime();
        for(Car car : cars)
        {
            carSkipList.search(car);
        }
        double t5 = System.nanoTime();
        for(Car car : cars)
        {
            carSkipList.delete(car);
        }
        double t6 = System.nanoTime();

        System.out.println(new String("-".repeat(100)));
        System.out.println("\nElement count: " + elementCount + "\nLinked list tests:\n ");
        System.out.println("Insert: " + Double.toString((t1-t0)/1e7)+ "\nSearch: " + Double.toString((t2-t1)/1e7) +  "\nDelete: " + Double.toString((t3-t2)/1e7));
        System.out.println("\nSkipListtests:\n ");
        System.out.println("Insert: " + Double.toString((t4-t3)/1e7)+ "\nSearch: " + Double.toString((t5-t4)/1e7) +  "\nDelete: " + Double.toString((t6-t5)/1e7));

    }

    void execute() {
        long memTotal = Runtime.getRuntime().totalMemory();
        // Pasižiūrime kaip generuoja automobilius (20) vienetų)
        generateCars(20);
        for (Car c : cars) {
            System.out.println(c);
        }

        for (int n : counts) {
            generateAndExecute(n);
        }

    }
    public static void main(String[] args) {
        // suvienodiname skaičių formatus pagal LT lokalę (10-ainis kablelis)
        Locale.setDefault(new Locale("LT"));
        new SimpleBenchmark().execute();
    }
}
