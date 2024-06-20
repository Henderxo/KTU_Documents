package models;

import utils.LinkList;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

public class StudentInformation implements  Comparable<StudentInformation> {

    public int Nr;
    public String Group;
    public int Count;
    public List<Integer> Grade = new ArrayList<>();

    public StudentInformation(int Nr, String Group, int Count, List<Integer> Gres)
    {
        this.Nr = Nr;
        this.Group = Group;
        this.Count = Count;
        for (int i = 0; i < Gres.size(); i++)
        {
            this.Grade.add(Gres.get(i));
        }
    }

    public List<Integer> GetGrades()
    {
        return Grade;
    }

    @Override
    public String toString()
    {
        int s = 0;
        String line = "";
        for (int Grade : Grade)
        {
            if(s == 0)
            {
                line = line + Grade;
                s++;
            }
            else
            {
                line = line + ", " + Grade;
                s++;
            }

        }
        return line;
    }

    @Override
    public boolean equals(Object b)
    {
        return ((StudentInformation) b).Nr == this.Nr;
    }


    @Override
    public int compareTo(StudentInformation o) {
        int ret = Integer.compare(o.Nr, this.Nr);
        return ret;
    }
}
