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
        LinkedInClasses json = linkedInAPI?.LoadJsonFile();
        if (json != null)
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
        LinkedInClasses json = linkedInAPI?.LoadJsonFile();
        Debug.Log("data from button: " + json.full_name);

        if (json != null)
        {
            FindObjectOfType<WatsonTTS>().SynthesizeAndPlayRequest(new WatsonRequest(json.full_name + "is some boy"));
        }
        else
        {
            Debug.LogWarning("No JSON data found or LinkedInAPI missing.");
        }       
    }
}
