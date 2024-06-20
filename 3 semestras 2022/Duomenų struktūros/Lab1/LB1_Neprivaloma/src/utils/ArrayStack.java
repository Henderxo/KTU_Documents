package utils;

import java.util.ArrayList;
import java.util.LinkedList;

public class ArrayStack <E> implements  Stack<E>
{
    private final ArrayList<E> List = new ArrayList<E>();

    @Override
    public E pop() {
        if(this.isEmpty())
        {
            return null;
        }
        E temp = List.get(List.size() - 1);
        List.remove(List.size() - 1);
        return temp;
    }

    @Override
    public void push(E item) {
        List.add(item);
    }

    @Override
    public E peak() {
        if(this.isEmpty())
        {
            return null;
        }
        return this.List.get(List.size() - 1);
    }

    @Override
    public boolean isEmpty() {
        if(this.List.isEmpty())
        {
            return true;
        }
        return false;
    }






}
