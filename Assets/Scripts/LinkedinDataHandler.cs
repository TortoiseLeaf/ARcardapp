using UnityEngine;

public class LinkedInDataHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the LinkedInAPI component for fetching/reading data.")]
    public LinkedInAPI linkedInAPI;



    // method to fetch the LinkedIn profile
    public void FetchLinkedInProfile()
    {
        if (linkedInAPI != null)
        {
            linkedInAPI.FetchLinkedInProfile();
        }
        else
        {
            Debug.LogWarning("No LinkedInAPI found. Cannot fetch data.");
        }        
    }

    // print the user's bio 
    public void PrintBio()
    {
        string json = linkedInAPI?.LoadJsonFile();
        if (!string.IsNullOrEmpty(json))
        {

            Debug.Log("Bio from JSON:  bio");
        }
        else
        {
            Debug.LogWarning("No JSON data found or LinkedInAPI missing.");
        }

       
    }

    // print the interest
    public void PrintInterest()
    {
        string json = linkedInAPI?.LoadJsonFile();
        if (!string.IsNullOrEmpty(json))
        {
            Debug.Log("Interest from JSON: interest");
        }
        else
        {
            Debug.LogWarning("No JSON data found or LinkedInAPI missing.");
        }

       
    }
}
