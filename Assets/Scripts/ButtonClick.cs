using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class ButtonClick : MonoBehaviour, IPointerClickHandler //, IPointerDownHandler, IPointerUpHandler
{
    string _linkedinApi = ConfigLoader.GetLinkedinApi();

    string _linkedinProfileUrl = System.Environment.GetEnvironmentVariable("LINKEDIN_PROFILE");

    [Tooltip("UnityEvent that fires when the object is clicked.")]
    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
        // invoking visual feedback
        OnQuadTouch(); 
    }

    public void OnQuadTouch()
    {
        StartCoroutine(ButtonClickEffect()); 
        Debug.Log("Quad was touched!");
        Debug.Log("persistent data path: " + Application.persistentDataPath);
        Debug.Log("env var " + _linkedinApi);
    }

    private IEnumerator ButtonClickEffect()
    {
        Vector3 originalScale = transform.localScale;
        // Shrink effect
        transform.localScale = originalScale * 0.9f; 
        yield return new WaitForSeconds(0.1f);
        // Restore size
        transform.localScale = originalScale; 
    }

    // handle touch manually (for non-UI objects)
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
                {
                    onClick?.Invoke();
                    OnQuadTouch();
                }
            }
        }
    }   
}