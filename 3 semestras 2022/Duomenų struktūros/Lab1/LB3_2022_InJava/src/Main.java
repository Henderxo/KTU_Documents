import models.Student;
import models.StudentInformation;
import utils.DataReader;
import utils.LinkList;
import utils.TaskUtils;
import java.io.File;


public class Main {
    public static void main(String[] args)
    {

        File file = new File("C:\\Users\\Lenovo\\IdeaProjects\\LB3_2022_InJava\\Ats.txt");
        if(file.exists())
        {
            file.delete();
        }

        LinkList<Student> List = DataReader.readFromFile1("U14a.txt");
        LinkList<StudentInformation> Info = DataReader.readFromFile2("U14b.txt");

        DataReader.WriteData("Ats.txt", List, "Information from the given file");

        TaskUtils.Distribution(List, Info);
        TaskUtils.AddFunds(List,Info);
        List.Sort();

        DataReader.WriteData("Ats.txt", List, "Sorted Student List");
        DataReader.WriteBsicData("Ats.txt", List, Info, "Testing sort and seeing how much cash each student got:");
        TaskUtils.RemoveStudents(List, Info);

        DataReader.WriteData("Ats.txt", List, "Student list after using remove");

        String group = DataReader.AskForGroupImput();
        LinkList<Student> ListByGroup = TaskUtils.FormGroupList(List, Info, group);
        DataReader.WriteBsicDataFrontRunners("Ats.txt", ListByGroup, Info, String.format("Group's %-1s students :", group ));


    }
}