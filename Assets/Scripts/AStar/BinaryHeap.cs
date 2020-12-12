using System;
using System.Collections.Generic;

public class BinaryHeap<TKey, TValue> where TKey : IComparable<TKey>
{
    private const int defaultItemCount = 64;
    private KeyValuePair<TKey, TValue> empty = new KeyValuePair<TKey, TValue>();
    private KeyValuePair<TKey, TValue>[] array;
    private int itemCount;

    public BinaryHeap() : this(defaultItemCount)
    {

    }
    public BinaryHeap(int capacity)
    {
        array = new KeyValuePair<TKey, TValue>[capacity];
    }

    public bool IsEmpty => itemCount == 0;
    public int Count => itemCount;

    public void Clear()
    {
        itemCount = 0;
        array = new KeyValuePair<TKey, TValue>[default];
    }

    private void Resize(int newSize)
    {
        var buffer = new KeyValuePair<TKey, TValue>[newSize];
        Array.Copy(array, 0, buffer, 0, itemCount);
        array = buffer;
    }

    private int GetParent(int i)
    {
        return (i - 1) / 2;
    }

    private int GetRight(int i)
    {
        return i * 2 + 2;
    }

    private int GetLeft(int i)
    {
        return i * 2 + 1;
    }

    public KeyValuePair<TKey, TValue> ExtractMax()
    {
        if (itemCount == 0) throw new Exception("Heap is empty");
        var result = array[0];
        array[0] = array[itemCount - 1];
        array[itemCount - 1] = empty;
        itemCount--;
        ShiftDown(0);
        return result;
    }

    public void Insert(TKey key, TValue value)
    {
        if (itemCount == array.Length)
            Resize(itemCount + defaultItemCount);
        itemCount++;
        array[itemCount - 1] = new KeyValuePair<TKey, TValue>(key, value);
        ShiftUp(itemCount - 1);
    }


    private void ShiftDown(int index)
    {
        var r = GetRight(index);
        var l = GetLeft(index);
        while (l < itemCount)
        {
            int indexMaxValue = -1;
            if (r >= itemCount)
            {
                if (array[l].Key.CompareTo(array[index].Key) < 0)
                {
                    indexMaxValue = l;
                }
            }
            else
            {
                if (array[r].Key.CompareTo(array[index].Key) < 0)
                {
                    indexMaxValue = r;
                    if (array[l].Key.CompareTo(array[r].Key) < 0)
                    {
                        indexMaxValue = l;
                    }
                }
                else
                {
                    if (array[l].Key.CompareTo(array[r].Key) < 0)
                    {
                        indexMaxValue = l;
                    }
                }
            }

            if (indexMaxValue == -1) break;

            var element = array[indexMaxValue];
            array[indexMaxValue] = array[index];
            array[index] = element;
            index = indexMaxValue;

            r = GetRight(index);
            l = GetLeft(index);
        }
    }

    private void ShiftDown3(int index)
    {
        while (index < itemCount &&
            (array[GetRight(index)].Key.CompareTo(array[index].Key) > 0 || array[GetLeft(index)].Key.CompareTo(array[index].Key) > 0)
        )
        {
            var r = GetRight(index);
            var l = GetLeft(index);

            var indexMaxValue = array[r].Key.CompareTo(array[l].Key) > 0 ? r : l;

            var element = array[indexMaxValue];
            array[indexMaxValue] = array[index];
            array[index] = element;
            index = indexMaxValue;
        }
    }

    private void ShiftDown2(int index)
    {
        while (index < itemCount &&
            (array[GetRight(index)].Key.CompareTo(array[index].Key) > 0 || array[GetLeft(index)].Key.CompareTo(array[index].Key) > 0)
        )
        {
            var r = GetRight(index);
            var l = GetLeft(index);
            if (array[r].Key.CompareTo(array[index].Key) > 0)
            {
                var rightElement = array[r];
                array[r] = array[index];
                array[index] = rightElement;
                index = r;
            }
            else
            {
                var leftElement = array[l];
                array[l] = array[index];
                array[index] = leftElement;
                index = l;
            }
        }
    }

    private void ShiftUp(int index)
    {
        while (array[GetParent(index)].Key.CompareTo(array[index].Key) > 0)
        {
            var parent = array[GetParent(index)];
            array[GetParent(index)] = array[index];
            array[index] = parent;
            index = GetParent(index);
        }
    }

    private void ChangePriority(int index, TKey newKey)
    {
        if (array[index].Key.CompareTo(newKey) < 0)
        {
            array[index] = new KeyValuePair<TKey, TValue>(newKey, array[index].Value);
            ShiftUp(index);
        }
        else if (array[index].Key.CompareTo(newKey) > 0)
        {
            array[index] = new KeyValuePair<TKey, TValue>(newKey, array[index].Value);
            ShiftDown(index);
        }
    }
}
