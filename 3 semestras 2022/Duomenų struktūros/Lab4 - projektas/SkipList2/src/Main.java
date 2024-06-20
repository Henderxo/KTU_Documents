import utils.LinkedList;
import utils.SkipNode;
import  utils.SkipList;
import Data.Car;
import utils.SkippableList;

public class Main {
    public static void main(String[] args) {

        Car c4 = new Car("Renault Laguna 2011 115900 700");
        Car c5 = new Car("Renault Megane 1946 365100 9500");
        Car c6 = new Car("Honda   Civic  2011  36400 80.3");
        Car c7 = new Car("Renault Laguna 2011 115900 7500");
        Car c8 = new Car("Renault Megane 1946 365100 950");
        Car c9 = new Car("Honda   Civic  2017  36400 850.3");

        SkipList<Car> lst = new SkipList<Car>(0.5);
        LinkedList<Car> lst2 = new LinkedList<>();

        lst.insert(c4);
        lst.insert(c5);
        lst.insert(c6);
        lst.insert(c7);
        lst.insert(c8);
        lst.insert(c9);
        lst.print();

        lst2.insert(c4);
        lst2.insert(c5);
        lst2.insert(c6);
        lst2.insert(c7);
        lst2.insert(c8);
        lst2.insert(c9);

        System.out.append(lst2.print());
        System.out.append("LinkedList Methods: ");

        System.out.println("Search: ");
        System.out.println(lst2.search(c4));
        System.out.println("Delete: ");
        System.out.println(lst2.delete(c4));
        System.out.append(lst2.print());

        lst.print();
        System.out.append("SkipList Methods: ");

        System.out.println("Search: ");
        System.out.println(lst.search(c4));
        System.out.println("Delete: ");
        System.out.println(lst.delete(c4));

        lst.print();
        System.out.println(lst.printNice());
    }
}