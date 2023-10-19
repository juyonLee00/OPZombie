using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Stack <T>
{
    private T[] popUp_UI;
    public int index;

    public int Size;

    public UI_Stack(int size)
    {
        Size = size;
        popUp_UI = new T[size];
        index = -1;
    }

    public void Push(T data)
    {
        if (Size - 1 == index)
        {
            throw new Exception("Stack overflow");
        }

        popUp_UI[++index] = data;
    }

    // 추출
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new Exception("Stack is Empty");
            return default(T);
        }

        return popUp_UI[index--];
    }

    // 체크
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new Exception("Stack is Empty");
            return default(T);
        }

        return popUp_UI[index];
    }

    // 비어있는지 체크
    public bool IsEmpty()
    {
        return index == -1;
    }
}
