#define DEBUG_MODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugString
{    
    public static void Log(object msg)
    {
#if DEBUG_MODE
        Debug.Log(msg);
#endif
    }
}
