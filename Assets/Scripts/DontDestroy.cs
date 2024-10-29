using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy
{
    public static DontDestroy instance = new DontDestroy();
    string status = "LET'S GO";
    public string GetNoti()
        { return status; }
    public void SetNoti(string s)
    {
        status = s;
    }
}
