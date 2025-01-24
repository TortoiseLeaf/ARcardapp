using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonClick : MonoBehaviour, IPointerClickHandler //, IPointerDownHandler, IPointerUpHandler
{
    public LinkedInAPI linkedInApi;


    void Update()
    {

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
        GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Quad was touched!");

    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Quad pressed via mouse/touch");
        GetComponent<Renderer>().material.color = Color.red;

        if (linkedInApi != null)
        {
            linkedInApi.FetchLinkedInProfile();
        }
        else
        {
            Debug.LogError("LinkedInAPI reference is missing!");
        }
    }
}