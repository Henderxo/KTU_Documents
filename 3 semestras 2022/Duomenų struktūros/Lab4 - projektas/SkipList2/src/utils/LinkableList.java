package utils;

public interface LinkableList<T extends  Comparable<T>> {



    boolean delete(T data);
    String print();
    void insert(T data);
    T search(T data);



}