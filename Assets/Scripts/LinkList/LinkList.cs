using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkList<T>:IListDS<T>
{
    private Node<T> head;//存储一个头结点
    private Node<T> tail;
    public LinkList()
    {
        head = null;
        tail = null;
    }

    public int GetLength()
    {
        if (head == null) return 0;
        Node<T> temp = head;
        int count = 1;
        while (true)
        {
            if (temp.Next != null)
            {
                count++;
                temp = temp.Next;
            }
            else
            {
                break;
            }
        }
        return count;
    }

    public void Clear()
    {
        head = null;
        tail = null;
    }

    public bool IsEmpty()
    {
        return head == null;
    }

    public void Add(T item)
    {
        Node<T> newNode = new Node<T>(item);//根据新的数据创建一个新的节点
        //设置头指针
        if (head == null)
        {   
            head = newNode;
        }
        //设置尾指针
        else
        {
            newNode.Pre = tail;
            tail.Next = newNode;
        }

        tail = newNode;
    }
    
    //删除后返回下一个节点
    public Node<T> Delete(Node<T> node)
    {
        //如果删除的是尾节点
        if(node.Next == null&&tail!=null)
        {
            tail = tail.Pre;
        }
        //如果删除的是头节点
        if(node.Pre==null&&head!=null)
        {   
            head = null;
            tail = null;
        }

        if(node.Pre!=null&&node.Next!=null)
        {    
            node.Pre.Next = node.Next;
            node.Next.Pre = node.Pre;
        }
        //要考虑后面全删除完的情况，这个时候前一个节点就要把Next置空
        else if(node.Pre!=null&&node.Next==null)
        {node.Pre.Next = null;}
        else{}
        
        var temp = node.Next;
        node.Pre = null;
        node.Next = null;
        return temp;
    }

    //对所有节点执行
    public void ProcessAllNode(Action<Node<T>> nodeCallFunc)
    {
        if(IsEmpty())
            return;
        Node<T> temp = head;
        while (temp!=null)
        {
            nodeCallFunc(temp);
            if (temp.Next != null)
                temp = temp.Next;
            else
                break;
        }
    }
    //边执行边删除满足条件的节点
    public void ProcessAllNodeWithDelete(Action<Node<T>> nodeCallFunc,Func<Node<T>,bool> deteleCondition)
    {
        if(IsEmpty())
            return;
            
        Node<T> temp = head;
        while (temp!=null)
        {
            nodeCallFunc(temp);
            //满足删除条件，删除后返回的是顶替原来节点的节点
            if(deteleCondition(temp))
                temp = Delete(temp);
            //不满足删除条件则直接跳过
            else
                temp = temp.Next;
        }
    }
}