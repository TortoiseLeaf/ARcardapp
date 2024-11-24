using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
public class LinkedInButton : MonoBehaviour,  IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerClick(PointerEventData data) 
    {
        Debug.Log("Quad pressed via mouse/touch on mobile!");
    }
    public void OnPointerUp(PointerEventData data) {}

    public void OnPointerDown(PointerEventData data) {}

}
*/

public class ButtonClick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    // Update is called once per frame
    void Update()
    {
        // Check for touches
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform == transform)
                    {
                        Debug.Log("Quad touched!");
                        OnQuadTouch();
                    }
                }
            }
        }
    }


    void OnQuadTouch()
    {
        GetComponent<Renderer>().material.color = Color.red; // Change color to red
        Debug.Log("Quad was touched!");

    }

    // Event-based interaction for the button
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Quad pressed via mouse/touch");
    }

    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("Pointer Up pressed.");
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Pointer Down pressed.");
    }
}
