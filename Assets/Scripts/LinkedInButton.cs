using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkedInButton : MonoBehaviour,  IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerClick(PointerEventData data) 
    {
        Debug.Log("Quad pressed via mouse/touch!");
    }
    public void OnPointerUp(PointerEventData data) {}

    public void OnPointerDown(PointerEventData data) {}

}
