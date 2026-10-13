using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomArray
{
    public static int[] StartShuffle(int CountArray)
    {
        int[] temp = new int[CountArray];
        for (int i = 0; i < CountArray; i++)
        {
            temp[i] = i + 1;
        }

        var temp1 = Shuffle(temp);
        return temp1;
    }

    public static int[] Shuffle(int[] array)
    {
        System.Random _random = new System.Random();
        var temp = array;
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + _random.Next(n - i);
            int t = temp[r];
            temp[r] = temp[i];
            temp[i] = t;
        }

        return array;
    }
}
