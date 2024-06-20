package utils;

import java.io.*;
import java.util.Scanner;

public class DataReader {

    public static String readFromFile(String filePath)
    {
        FileInputStream fileStream = null;
        Scanner scanner = null;
        String Line = "";


        try{
            fileStream = new FileInputStream(filePath);
            scanner = new Scanner(fileStream, "UTF-8");

            while(scanner.hasNextLine())
            {
                Line = scanner.nextLine();
            }

        }
        catch(Exception e)
        {
            e.printStackTrace();;
        }
        finally {
            if(scanner != null)
            {
                scanner.close();
            }

        }
        return Line;
    }

}
