using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTargetFoundAnimations : MonoBehaviour
{
    public GameObject linkedinButton;
    public GameObject bioButton;
    public GameObject interestButton;
    private Vector3 up = new Vector3(0, 0, 1);
    private Vector3 down = new Vector3(0, 0, -1);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    void Start()
    {       
        
        if (linkedinButton == null) linkedinButton = GameObject.Find("LinkedInButton");
        if (bioButton == null) bioButton = GameObject.Find("BioButton");
        if (interestButton == null) interestButton = GameObject.Find("InterestButton");
    }

    IEnumerator MoveItem(GameObject item)
    {
        while (Mathf.Abs(item.transform.position.z + 91f) > 0.001f)
        {            
            item.transform.position -= new Vector3(0, 0, 0.01f);
            yield return null;
        }
    }


    public void StartMoveItem()
    {   
        StartCoroutine(MoveItem(linkedinButton));
        StartCoroutine(MoveItem(bioButton));
        StartCoroutine(MoveItem(interestButton));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
