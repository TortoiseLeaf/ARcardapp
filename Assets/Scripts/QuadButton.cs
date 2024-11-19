/* using System.Collections;
using System.Collections.Generic;
using UnityEngine; */

/* public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
} */
using UnityEngine;

public class QuadButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect touch or left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray from screen to world
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform) // Check if this quad was hit
                {
                    Debug.Log("Quad pressed via mouse/touch!");
                }
            }
        }
    }
}
