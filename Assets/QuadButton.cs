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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    Debug.Log("Quad pressed via touch!");
                }
            }
        }
    }
}
