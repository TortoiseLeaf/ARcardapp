using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log($"Touch detected! Position neww: {Input.GetTouch(0).position}");
        }
        else
        {
            Debug.Log("No touch detected.");
        }
    }

}
