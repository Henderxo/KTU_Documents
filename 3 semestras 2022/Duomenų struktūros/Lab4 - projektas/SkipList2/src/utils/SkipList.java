package utils;

public class SkipList<T extends Comparable<T>> implements SkippableList<T> {

    int currentLevel;
    int MaxLvl;
    double p;

    SkipNode<T> header;

    public SkipList(double p)
    {
        this.MaxLvl = SkippableList.Levels;
        this.p = p;
        currentLevel = 0;

        header = new SkipNode<>(null, MaxLvl);

    }


    private int randomLevel()
    {
        double r = Math.random();
        int lvl = 0;
        while(r <= p && lvl < MaxLvl)
        {
            lvl ++;
            r = Math.random();
        }
        return lvl;
    }

    public SkipNode<T> createNode(T data, int level)
    {
        SkipNode<T> n = new SkipNode<T>(data, level);
        return n;
    }




    @Override
    public boolean delete(T data) {
        SkipNode<T> current = header;
        SkipNode<T>[] update = (SkipNode<T>[]) new SkipNode[MaxLvl + 1];

        for(int i = currentLevel; i >= 0; i--)
        {
            while(current.next[i] != null && current.next[i].data.compareTo(data) < 0)
            {
                current = current.next[i];
            }
            update[i] = current;
        }

        current = current.next[0];



        if(current.data == data)
        {
            for(int i = 0; current.level >= i; i++)
            {


                if(update[i].next[i] != current)
                {
                    break;
                }
                update[i].next[i] = current.next[i];

            }
            while(currentLevel > 0 && header.next[currentLevel] == null)
            {
                currentLevel = currentLevel - 1;
            }
            return true;
        }
        return false;
    }

    @Override
    public void print() {
        System.out.println("\n-----------SkipList-----------");
        for(int i = 0; i <= currentLevel; i++)
        {
            SkipNode<T> node = header.next[i];
            System.out.println("Level " + i + ": ");
            while(node != null)
            {
                System.out.println(node.data + " ");
                node = node.next[i];
            }
            System.out.println("");
        }
    }


    public String printNice() {
        System.out.println("\n-----------SkipList-----------");
        StringBuilder line = new StringBuilder();
        for(int i = currentLevel; i >= 0; i--)
        {

            SkipNode<T> node = header.next[0];
            SkipNode<T> upperNode = header.next[i];
            line.append("\n");
            boolean firstElement = true;


            while(node != null)
            {
                if(firstElement)
                {
                    line.append("Level: "+ i +"  | null->");
                }

                if(i == 0 && i != currentLevel)
                {

                    if(node.next[0] == null)
                    {
                        line.append(node.data + "->null");

                    }
                    else
                    {
                        line.append(node.data + "->");
                    }

                }


                if(i > 0)
                {




                    if(upperNode.next[i] == null)
                    {
                        if(upperNode.data.compareTo(node.data) == 0)
                        {
                            line.append(node.data);
                            if(node.next == null)
                            {
                                line.append("->null");
                            }




                            break;
                        }
                        if(upperNode.data.compareTo(node.next[0].data) != 0)
                        {
                            int count = node.data.toString().length();
                            int z = 0;
                            while (z != count)
                            {
                                z++;
                                line.append("-");
                            }
                            line.append("--");
                        }
                        else
                        {
                            int count = node.data.toString().length();
                            int z = 0;
                            while (z != count)
                            {
                                z++;
                                line.append("-");
                            }
                            line.append("->");
                        }

                    }
                    else if(upperNode.data.compareTo(node.data) == 0)
                    {

                        if(upperNode.next[i].data.compareTo(node.next[0].data) == 0)
                        {
                            line.append(node.data + "->");
                        }
                        else
                        {
                            line.append(node.data + "--");
                        }
                        upperNode = upperNode.next[i];
                    }
                    else if(upperNode.data.compareTo(node.next[0].data) == 0)
                    {
                        int count = node.data.toString().length();
                        int z = 0;
                        while (z != count)
                        {
                            z++;
                            line.append("-");
                        }
                        line.append("->");

                    }
                    else if(upperNode.next[i].data.compareTo(node.next[0].data) != 0)
                    {
                        int count = node.data.toString().length();
                        int z = 0;
                        while (z != count)
                        {
                            z++;
                            line.append("-");
                        }
                        line.append("--");

                    }
                    else if(upperNode.next[i].data.compareTo(node.next[0].data) == 0)
                    {
                        int count = node.data.toString().length();
                        int z = 0;
                        while (z != count)
                        {
                            z++;
                            line.append("-");
                        }
                        line.append("->");
                        upperNode = upperNode.next[i];
                    }
                }

                firstElement = false;
                node = node.next[0];

            }

        }
        return line.toString();
    }



    @Override
    public void insert(T data) {
        SkipNode<T> current = header;
        SkipNode<T>[] update = (SkipNode<T>[]) new SkipNode[MaxLvl + 1];

        for(int i = currentLevel; i >= 0; i--)
        {
            while(current.next[i] != null && current.next[i].data.compareTo(data) < 0)
            {
                current = current.next[i];
            }
            update[i] = current;
        }

        current = current.next[0];
        if((current == null) || current.data != data){
            int randomLevel = randomLevel();

            if(randomLevel > currentLevel)
            {
                for(int i = currentLevel + 1; i < randomLevel + 1; i++)
                {
                    update[i] = header;

                }
                currentLevel = randomLevel;
            }

            SkipNode<T> n = createNode(data, randomLevel);

            for(int i = 0; i <= randomLevel; i++)
            {
                n.next[i] = update[i].next[i];
                update[i].next[i] = n;
            }

        }
    }

    @Override
    public T search(T data) {
        SkipNode<T> current = header;
        for(int i = currentLevel; i >= 0; i--)
        {
            while(current.next[i] != null && current.next[i].data.compareTo(data) <= 0)
            {
                current = current.next[i];
            }
        }
        if(current.data.compareTo(data) == 0)
        {
            return current.data;
        }
        return null;
    }
}