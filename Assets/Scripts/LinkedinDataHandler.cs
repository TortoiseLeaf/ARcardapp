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

    // play the user's bio 
    public void PlayBio()
    {        
        string json = linkedInAPI?.LoadJsonFile();
        if (!string.IsNullOrEmpty(json))
        {
            FindObjectOfType<WatsonTTS>().SynthesizeAndPlayRequest(new WatsonRequest("Bio here."));
        }
        else
        {
            Debug.LogWarning("No JSON data found or LinkedInAPI missing.");
        }       
    }

    // play the interest
    public void PlayInterest()
    {   
        string json = linkedInAPI?.LoadJsonFile();
        if (!string.IsNullOrEmpty(json))
        {
            FindObjectOfType<WatsonTTS>().SynthesizeAndPlayRequest(new WatsonRequest("Interest here."));
        }
        else
        {
            Debug.LogWarning("No JSON data found or LinkedInAPI missing.");
        }       
    }
}
