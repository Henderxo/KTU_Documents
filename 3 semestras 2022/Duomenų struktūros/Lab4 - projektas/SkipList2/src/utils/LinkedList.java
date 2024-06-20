package utils;

import java.nio.file.NotDirectoryException;

public class LinkedList <T extends Comparable<T>> implements  LinkableList<T>{

    private Node<T> first;
    private Node<T> last;
    private Node<T> current;
    private int size;

    public LinkedList()
    {

    }

    public int GetSize()
    {
        return size;
    }

    @Override
    public boolean delete(T data) {
        Node<T> pren = first;
        if(first.data.compareTo(data) == 0)
        {
            first = first.next;
            size--;
            return true;
        }
        for(Node<T> n = first; n!=null; n = n.next)
        {
            if(n.data.compareTo(data) == 0)
            {
                pren.next = n.next;
                n = null;
                size--;
                return true;
            }
            pren = n;
        }
        return false;
    }

    @Override
    public String print() {

        StringBuilder line = new StringBuilder();
        line.append("\n-----------LinkedList-----------\n");
        for(Node<T> n = first; n!= null; n = n.next)
        {
            line.append(n.data.toString() + "->");
        }
        line.append("null\n");

        return  line.toString();
    }

    @Override
    public void insert(T data) {

        if (data == null) {
            throw new RuntimeException("Given element is null");
        }
        if(first == null)
        {
            first = new Node<>(data, first);
            last = first;
            size++;
        }
        else {

            Node<T> node = new Node<>(data, null);
            for(Node<T> n = first; n != null; n = n.next)
            {
                if(n.data.compareTo(node.data) > 0 && n == first)
                {
                    node.next = first;
                    first = node;
                    break;
                }

                if(n.next == null)
                {
                    n.next = node;
                    break;
                }
                if(n.data.compareTo(data) < 0 && n.next.data.compareTo(data)  > 0)
                {

                    node.next = n.next;
                    n.next = node;
                    size++;
                    break;
                }
            }
        }
    }

    @Override
    public T search(T data) {
        for(Node<T> n = first; n!= null; n = n.next)
        {
            if(data.compareTo(n.data) == 0)
            {
                return n.data;
            }
        }
        return null;
    }

    private static class Node<T> {
        private final T data;
        private Node<T> next;

        Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }
    }
}