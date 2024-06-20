package models;

public class Student implements  Comparable<Student> {

    public int Nr;
    public String SurName;
    public String Name;
    public int Phone;
    public double Pirm;
    public double CashGiven;

    public Student(int Nr, String SurName, String Name, int Phone)
    {
        this.Nr = Nr;
        this.SurName = SurName;
        this.Name = Name;
        this.Phone = Phone;
        this.Pirm = 0;
        this.CashGiven = 0;
    }
    @Override
    public boolean equals(Object student)
    {
        return ((Student) student).Nr == (this.Nr);
    }

    @Override
    public String toString()
    {
        return String.format("| %-8s | %-12s | %-14s | %-12s |\n",
                Nr, SurName, Name, Phone);
    }



    public int compareTo(Student b)
    {
        int ret = Double.compare(b.CashGiven, CashGiven);
        if (ret == 0)
        {
            ret = SurName.compareTo(b.SurName);
            if (ret == 0)
            {
               ret = Name.compareTo(b.Name);
            }
        }
        return ret;
    }


}
