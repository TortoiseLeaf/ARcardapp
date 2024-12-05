using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LinkedInAPI : MonoBehaviour
{
    private readonly string _apiKey = "OWkz-KuGk-CoSzhvRagy8g";
    private readonly string _baseUrl = "https://nubela.co/proxycurl/api/v2/linkedin";

    private readonly string linkedinProfileUrl = "https://www.linkedin.com/in/lauren-keenan-1426111b4/";

    // use the function outside of the file
    public void FetchLinkedInProfile()
    {
        StartCoroutine(GetLinkedInProfile());
    }

    public IEnumerator GetLinkedInProfile()
    {

        string requestUrl = $"{_baseUrl}?url={UnityWebRequest.EscapeURL(linkedinProfileUrl)}";
        
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        request.SetRequestHeader("Authorization", $"Bearer {_apiKey}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("success api");
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}
