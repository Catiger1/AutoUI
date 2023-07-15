using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    private T data;//存储数据
    private Node<T> next;//指针 用来指向下一个元素
    private Node<T> pre;
    public Node()
    {
        data = default(T);
        next = null;
        pre = null;
    }

    public Node(T value)
    {
        data = value;
        pre = null;
        next = null;
    }

    public Node(T value, Node<T> pre,Node<T> next)
    {
        this.data = value;
        this.next = next;
        this.pre = pre;
    }

    public Node(Node<T> pre,Node<T> next)
    {
        this.pre = pre;
        this.next = next;
    }

    public T Data
    {
        get { return data; }
        set { data = value; }
    }

    public Node<T> Next
    {
        get { return next; }
        set { next = value; }
    }
    public Node<T> Pre
    {
        get { return pre; }
        set { pre = value; }
    }
}
