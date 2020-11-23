using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static EventDelegate.Parameter MakeParameter(Object _value, System.Type _type)
    {
        EventDelegate.Parameter param = new EventDelegate.Parameter();

        param.obj = _value;
        param.expectedType = _type;
        return param;
    }
    public static int GetPriority(int[] table)
    {
        if (table == null || table.Length == 0) return -1;

        int sum = 0;
        int num = 0;
        for(int i = 0; i< table.Length; i++)
        {
            sum += table[i];
        }
        num = Random.Range(1, sum + 1);
        sum = 0;
        for(int i = 0; i< table.Length; i++)
        {
            if (num > sum && num <= sum + table[i])
            {
                return i;
            }
            sum += table[i];
        }
        return -1;
    }
}
