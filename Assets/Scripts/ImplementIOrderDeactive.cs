using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplementIOrderDeactive : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour[] gObj;
    private void OnDisable()
    {
        for (int i = 0; i < gObj.Length; i++)
        {
            if (gObj[i] is IOderDeactive o)
            {
                o._OnDisable();
            }
        }
    }
}
