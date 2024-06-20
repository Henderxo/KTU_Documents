package utils;

public class TaskUtils {



    public static boolean Calculator(String line)
    {
        boolean returnvalue = false;
        if(line.length() %2 != 0)
        {
            return false;
        }
        else
        {
            ArrayStack<Integer> Stack = new ArrayStack<Integer>();
            for(int i = 0; i < line.length(); i++)
            {

                int number = (int) line.charAt(i);
                if(Stack.isEmpty())
                {
                    Stack.push(number);
                }
                else
                {
                    if(Stack.peak() == number-1 || Stack.peak() == number-2)
                    {
                        Stack.pop();
                    }
                    else
                    {
                        Stack.push(number);
                    }
                }
            }
            if(Stack.isEmpty())
            {
                returnvalue = true;
            }
        }



        return returnvalue;
    }


}
