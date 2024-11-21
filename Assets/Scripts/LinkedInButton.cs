using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkedInButton : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData data) 
    {
        Debug.Log("Quad pressed via mouse/touch!");
    }
}
