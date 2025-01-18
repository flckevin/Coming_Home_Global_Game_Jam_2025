using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuanhLogger
{
    private static bool log = true;
    public static void Log(object _message)
    {
        if(log == false) return;
        Debug.Log(_message);
    }
}
