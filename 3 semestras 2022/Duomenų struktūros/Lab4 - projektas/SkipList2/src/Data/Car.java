package Data;

import utils.P;

import java.util.InputMismatchException;
import java.util.NoSuchElementException;
import java.util.Random;
import java.util.Scanner;

public final class Car implements Comparable<Car>, P<Car> {

    private static int serNr = 100;

    private static final String idCode = "TA";
    private final String carRegNr;

    private String make = "";
    private String model = "";
    private int year = -1;
    private int mileage = -1;
    private double price = -1.0;

    public Car() {
        carRegNr = idCode + serNr++;
    }

    public Car(String dataString) {
        Random rand = new Random();
        carRegNr = idCode +( (serNr++) + rand.nextInt(100));    // suteikiamas originalus carRegNr
        this.parse(dataString);

    }

    public Car(int codeAdder) {
        carRegNr = idCode + (serNr + codeAdder);
    }

    public Car(String make, String model, int year, int mileage, double price, int codeadder) {
        carRegNr = idCode + (serNr + codeadder);
        this.make = make;
        this.model = model;
        this.year = year;
        this.mileage = mileage;
        this.price = price;

    }

    public String getMake() {
        return make;
    }

    public String getModel() {
        return model;
    }

    public int getYear() {
        return year;
    }

    public int getMileage() {
        return mileage;
    }

    public void setMileage(int mileage) {
        this.mileage = mileage;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String getCarRegNr() {  //** nauja.
        return carRegNr;
    }

    @Override
    public String toString() {  // papildyta su carRegNr
        return getCarRegNr() + "=" + make + "_" + model + ":" + year + " " + getMileage() + " " + String.format("%4.1f", price);
    }

    @Override
    public int compareTo(Car car) {
        return getCarRegNr().compareTo(car.getCarRegNr());
    }

    @Override
    public void parse(String dataString) {
        try {   // duomenys, atskirti tarpais
            Scanner scanner = new Scanner(dataString);
            make = scanner.next();
            model = scanner.next();
            year = scanner.nextInt();
            setMileage(scanner.nextInt());
            setPrice(scanner.nextDouble());
        } catch (InputMismatchException e) {
            e.toString();
        } catch (NoSuchElementException e) {
            e.getMessage();
        }
    }
}