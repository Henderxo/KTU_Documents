package utils;

public class SkipNode<N extends Comparable<N>> {

    protected  N data;
    protected  int level;
    protected  SkipNode<N>[] next;

    protected  SkipNode()
    {

    }


    protected SkipNode(N data, int level)
    {
        this.data = data;
        this.level = level;
        this.next = new SkipNode[level + 1];
    }



}