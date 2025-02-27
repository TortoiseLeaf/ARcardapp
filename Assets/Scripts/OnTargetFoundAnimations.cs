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

    IEnumerator MoveItem(GameObject item, Vector3 direction, float speed, Vector3 finalPosition)
    {
        while (Vector3.Distance(item.transform.position, finalPosition) > 0.01f)
        {
            item.transform.position += direction * speed;
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void StartMoveItem()
    {   
        StartCoroutine(MoveItem(linkedinButton, down, 0.1f, new Vector3(0, 0, -5)));
        StartCoroutine(MoveItem(bioButton, down, 0.1f, new Vector3(0, 0, -5)));
        StartCoroutine(MoveItem(interestButton, down, 0.1f, new Vector3(0, 0, -5)));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
