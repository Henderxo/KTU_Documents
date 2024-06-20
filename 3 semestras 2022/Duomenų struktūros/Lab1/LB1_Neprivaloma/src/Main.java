import utils.DataReader;
import utils.TaskUtils;

public class Main {
    private final static String filePath  = "TheLine.txt";
    public static void main(String[] arg)
    {

        String Line = DataReader.readFromFile(filePath);
        if(TaskUtils.Calculator(Line))
        {
            System.out.println("True");
        }
        else
        {
            System.out.println("False");
        }
    }



}