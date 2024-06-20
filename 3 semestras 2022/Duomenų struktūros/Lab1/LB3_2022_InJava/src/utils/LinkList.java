package utils;

import java.util.Enumeration;
import java.util.Iterator;

public class LinkList<type extends Comparable<type>> implements Iterable<type>
{



    @Override
    public Iterator<type> iterator() {
        return new Iterator<type>() {

            Node current;
            {
                this.current = LinkList.this.head;
            }

            @Override
            public boolean hasNext() {
                return this.current != null;
            }

            @Override
            public type next() {
                if(this.hasNext())
                {
                    type data = this.current.Data;
                    this.current = this.current.Link;
                    return data;
                }
                else
                {
                    return null;
                }
            }
        };
    }

    public void Begin()
    {
        d = head;
    }

    public void Next()
    {
        d = d.Link;
    }

    public boolean Exist()
    {
        return d != null;
    }


    private class Node
    {

        public type Data;
        public Node Link;
        public Node()
        {

        }

        public Node (type value, Node link)
        {
            this.Data = value;
            this.Link = link;
        }
    }

    private Node head;
    private Node tail;
    private Node d;
    public int FundSize;
    public double Vid;

    public void AddFundSizeAndVid(int Size, double Vid)
    {
        this.FundSize = Size;
        this.Vid = Vid;
    }

    public LinkList()
    {
        this.head = null;
        this.tail = null;
        this.d = null;

    }

    public void Setinfo(type student)
    {
        head = new Node(student, head);
    }

    private void LastElemenet()
    {
        Node curr = head;
        while (curr.Link != null)
        {
            curr = curr.Link;
        }
        tail = curr;
    }

    public void Add(type k)
    {
        Node NewNode = new Node(k, null);
        if(this.head == null)
        {
            this.head = NewNode;
        }
        else {
            for(Node d = this.head; d != null; d = d.Link)
            {
                if(d.Link == null)
                {
                    d.Link = NewNode;
                    return;
                }
            }
        }


    }

    public void Remove(type Nr)
    {
        Node d, s;
        d = s = head;
        if (s.Link != null || d == null)
        {
            while (s.Link != null)
            {
                s = d.Link;
                if (d.Data.equals(Nr))
                {
                    d.Data = s.Data;
                    d.Link = s.Link;
                    s = null;
                    break;
                }
                if (s.Data.equals(Nr) && s.Link == null)
                {
                    s = null;
                    d.Link = s;
                    break;
                }
                d = s;
            }
        }
        else
        {
            if (d != null)
            {
                d = null;
            }
        }
    }




    public void Sort()
    {
        if(head == null)
        {
            return;
        }
        boolean done = true;
        while(done)
        {
            done = false;
            var headn = head;
            while(headn != null)
            {
                if(headn.Link != null && headn.Data.compareTo(headn.Link.Data) > 0)
                {
                    type St = headn.Data;
                    headn.Data = headn.Link.Data;
                    headn.Link.Data = St;
                    done = true;

                }
                headn = headn.Link;
            }
        }
    }

    public type Get()
    {
        return this.d.Data;
    }

}
