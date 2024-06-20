package utils;

import models.Student;
import models.StudentInformation;

public class TaskUtils {

    public static void Distribution (LinkList<Student> List1, LinkList<StudentInformation> List2)
    {
        for(List1.Begin(); List1.Exist(); List1.Next())
        {
            Student student = List1.Get();
            StudentInformation info = TaskUtils.FindNumber(student.Nr, List2);

            double Count = 0;
            double Sum = 0;
            boolean Debt = false;
            for (int i = 0; i < info.GetGrades().size(); i++)
            {
                if(info.Grade.get(i) >= 4.5)
                {
                    Sum += info.Grade.get(i);
                    Count++;
                }
                else
                {
                    Debt = true;
                    break;
                }
            }
            if (Sum > 0 && (Sum/Count) > List2.Vid && Debt == false)
            {
                if(Sum / Count >= 8.5)
                {
                    student.Pirm = 1.1;
                }
                else
                {
                    student.Pirm = 1;
                }
            }


        }


    }

    private static double CalculateIndex(LinkList<Student> List1, LinkList<StudentInformation> List2)
    {
        double SumIndex = 0;
        for (List1.Begin(); List1.Exist(); List1.Next())
        {
            Student student = List1.Get();
            SumIndex += student.Pirm;
        }
        return SumIndex;
    }

    public static void AddFunds(LinkList<Student> List1, LinkList<StudentInformation> List2)
    {
        double SumIndex = TaskUtils.CalculateIndex(List1, List2);
        double IndexWorth = List2.FundSize / SumIndex;
        for(List1.Begin(); List1.Exist(); List1.Next())
        {
            Student student = List1.Get();
            student.CashGiven = student.Pirm * IndexWorth;
        }
    }

    public static void RemoveStudents(LinkList<Student> List1, LinkList<StudentInformation> List2)
    {
        boolean reset = true;
        while(reset)
        {
            reset = false;
            for(List1.Begin(); List1.Exist(); List1.Next())
            {
                Student student = List1.Get();
                if (student.CashGiven == 0)
                {
                    List2.Remove(TaskUtils.FindNumber(student.Nr, List2));
                    List1.Remove(student);
                    reset = true;
                    break;
                }
            }
        }
    }

    public static StudentInformation FindNumber(int Nr, LinkList<StudentInformation> Info) {
        for (Info.Begin(); Info.Exist(); Info.Next())
        {

            if (Nr == Info.Get().Nr)
            {

                return Info.Get();
            }
        }
        return Info.Get();


    }
    public static Student FindNumber2(int Nr, LinkList<Student> List)
    {


        for (List.Begin(); List.Exist(); List.Next())
        {
            if (Nr == List.Get().Nr)
            {
                return List.Get();
            }
        }


        return List.Get();
    }

    public static LinkList<Student> FormGroupList(LinkList<Student> List, LinkList<StudentInformation> Info, String GroupNeeded)
    {
        LinkList<Student> GroupedList = new LinkList<Student>();
        for (Info.Begin(); Info.Exist(); Info.Next())
        {


            StudentInformation info = Info.Get();

            if(info.Group.equals(GroupNeeded))
            {

                Student student = TaskUtils.FindNumber2(info.Nr, List);
                GroupedList.Add(student);
            }
        }
        return GroupedList;
    }

}


