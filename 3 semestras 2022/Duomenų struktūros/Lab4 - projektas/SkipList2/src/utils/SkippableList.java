package utils;

public interface SkippableList<T extends  Comparable<T>> {

    int Levels = 15;

    boolean delete(T data);
    void print();
    void insert(T data);
    T search(T data);



}