package utils;

import models.Student;
import models.StudentInformation;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.io.BufferedWriter;
import javax.swing.JOptionPane;

public class DataReader {
    public static LinkList<Student> readFromFile1(String filePath)
    {
        FileInputStream fileStream = null;
        Scanner scanner = null;
        LinkList<Student> List = new LinkList<Student>();

        try{
            fileStream = new FileInputStream(filePath);
            scanner = new Scanner(fileStream, "UTF-8");

            while(scanner.hasNextLine())
            {

                String[] Lines = scanner.nextLine().split(" ");
                int Nr = Integer.parseInt(Lines[0]);
                String SurName = Lines[1];
                String Name = Lines[2];
                int Phone = Integer.parseInt(Lines[3]);
                List.Add(new Student(Nr, SurName, Name, Phone));

            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        finally {
            if(scanner != null)
            {
                scanner.close();
            }
        }
        return List;
    }

    public static LinkList<StudentInformation> readFromFile2(String filePath)
    {

        FileInputStream fileStream = null;
        Scanner scanner = null;
        LinkList<StudentInformation> List = new LinkList<StudentInformation>();

        try{
            fileStream = new FileInputStream(filePath);
            scanner = new Scanner(fileStream, "UTF-8");
            int Linecount = 0;
            int CaseFound = 0;
            while(scanner.hasNextLine())
            {
                if(Linecount == 0) {
                    String[] Words = scanner.nextLine().split(" ");
                    List.AddFundSizeAndVid(Integer.parseInt(Words[0]),
                            Double.parseDouble(Words[1]));
                    Linecount++;
                    CaseFound++;
                    continue;

                }
                if(CaseFound == 1)
                {
                    List<Integer> Grades = new ArrayList<>();
                    String[] Words = scanner.nextLine().split(" ");
                    int Nr = Integer.parseInt((Words[0]));
                    String Group = Words[1];
                    int Count = Integer.parseInt((Words[2]));
                    for(int l = 3; l < Count + 3; l++)
                    {
                        Grades.add((Integer.parseInt(Words[l])));
                    }
                    List.Add(new StudentInformation(Nr, Group, Count, Grades));
                }


            }
        }
        catch (Exception e){
            e.printStackTrace();
        }
        finally {
            if(scanner != null)
            {
                scanner.close();
            }
        }

        return List;
    }

    public static void WriteData (String Filename, LinkList<Student> List, String Header)
    {
        try{
            BufferedWriter file = new BufferedWriter(new FileWriter(Filename, StandardCharsets.UTF_8, true));
            file.write(String.format(Header + "\n"));
            file.write(String.format(new String("-".repeat(59)) + "\n"));
            file.write(String.format("| %-8s |" +
                            " %-12s | %-14s | %-12s |\n", "Numeris",
                    "Pavardė", "Vardas", "Telefonas"));
            file.write(String.format(new String("-".repeat(59)) + "\n"));
            for (Student student: List)
            {
                file.write(String.format(student.toString()));
            }
            file.write(String.format(new String("-".repeat(59)) + "\n"));

            file.close();

        }
        catch(Exception e)
        {
            e.getStackTrace();
        }

    }


    public static void WriteBsicData (String Filename, LinkList<Student> List, LinkList<StudentInformation> Info, String Header)
    {
        try{
            BufferedWriter file = new BufferedWriter(new FileWriter(Filename, StandardCharsets.UTF_8, true));
            file.write(String.format(Header + "\n"));
            file.write(String.format(new String("-".repeat(89)) + "\n"));
            file.write(String.format("| %-8s | %-12s" +
                            " | %-14s | %-20s | %-20s |\n", "Numeris",
                    "Pavardė", "Vardas", "Pažymiai", "Stipendija"));
            file.write(String.format(new String("-".repeat(89)) + "\n"));
            for (Student student: List)
            {
                file.write(String.format("| %-8s |" +
                                " %-12s | %-14s | %-20s | %-20s |\n",
                        student.Nr, student.SurName, student.Name,
                        TaskUtils.FindNumber(student.Nr, Info).toString(), student.CashGiven));
            }
            file.write(String.format(new String("-".repeat(89)) + "\n"));

            file.close();

        }
        catch(Exception e)
        {
            e.getStackTrace();
        }




    }

    public static void WriteBsicDataFrontRunners (String Filename, LinkList<Student> List, LinkList<StudentInformation> Info, String Header)
    {
        try{
            BufferedWriter file = new BufferedWriter(new FileWriter(Filename, StandardCharsets.UTF_8, true));
            file.write(String.format(Header + "\n"));
            file.write(String.format(new String("-".repeat(56)) + "\n"));
            file.write(String.format("| %-12s |" +
                            " %-14s | %-20s |\n", "Paverdė", "Vardas", "Pažymiai"));
            file.write(String.format(new String("-".repeat(56)) + "\n"));
            for (Student student: List)
            {
                file.write(String.format("| %-12s |" +
                                " %-14s | %-20s |\n", student.SurName, student.Name,
                        TaskUtils.FindNumber(student.Nr, Info).toString()));
            }
            file.write(String.format(new String("-".repeat(56)) + "\n"));

            file.close();

        }
        catch(Exception e)
        {
            e.getStackTrace();
        }




    }

    public static String AskForGroupImput()
    {
        System.out.println("Write the group you want so sort by: ");
        Scanner grade = new Scanner(System.in);
        return (String) grade.nextLine();

    }
}
