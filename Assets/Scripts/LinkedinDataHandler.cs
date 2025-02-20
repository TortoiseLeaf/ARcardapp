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

    // play the user's bio ( I don't have a bio so have used education )
    public void PlayBio()
    {        
        LinkedInClasses json = linkedInAPI?.LoadJsonFile();
        if (json != null)
        {
            Debug.Log("education history: " + json.education[0].degree);
            FindObjectOfType<WatsonTTS>().SynthesizeAndPlayRequest(new WatsonRequest("Student of " + json.education[0].degree + "at " + json.education[0].school));
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
