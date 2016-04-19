using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemsCount;

    public Heap(int maxHeapsize)
    {
        items = new T[maxHeapsize];
    }

    public void Add(T item)
    {
        item.HeapIndex = currentItemsCount;
        items[currentItemsCount] = item;
        SortUp(item);
        currentItemsCount++;
    }    
    
    public T RemoveFirstItem()
    {
        T firstItem = items[0];
        currentItemsCount--;
        items[0] = items[currentItemsCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    } 

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemsCount;
        }
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentItemsCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < currentItemsCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight])<0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if(item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex-1)/2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
