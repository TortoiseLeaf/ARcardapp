using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class LinkedInAPI : MonoBehaviour
{
    private ProxycurlCredentials credentials;
    private string credentialsFilePath;

    public string jsonFilePath;

    void Awake()
    {
        //get and set paths for credentials and api response
        credentialsFilePath = Path.Combine(Application.streamingAssetsPath, "credentials.json");

        jsonFilePath = Path.Combine(Application.persistentDataPath, "LinkedInProfile.json");

        LoadCredentials();
    }

    private void LoadCredentials()
    {
        try
        {
            if (File.Exists(credentialsFilePath))
            {
                string json = File.ReadAllText(credentialsFilePath);
                credentials = JsonUtility.FromJson<ProxycurlCredentials>(json);

                Debug.Log("Credentials loaded successfully.");
            }
            else
            {
                Debug.LogError("Credentials file not found!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading credentials: {e.Message}");
        }
    }

    public void FetchLinkedInProfile()
    {
        if (credentials == null || string.IsNullOrEmpty(credentials._apiKeyLinkedIn))
        {
            Debug.LogError("API credentials are not set!");
            return;
        }

        StartCoroutine(GetLinkedInProfile());
    }

    private IEnumerator GetLinkedInProfile()
    {

        string requestUrl = $"{credentials._baseUrlLinkedIn}?url={UnityWebRequest.EscapeURL(credentials._linkedinProfileUrl)}";
        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        request.SetRequestHeader("Authorization", $"Bearer {credentials._apiKeyLinkedIn}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + request.downloadHandler.text);
            string response = request.downloadHandler.text;

            try
            {
                // deserialise 
                LinkedInClasses linkedInClasses = JsonConvert.DeserializeObject<LinkedInClasses>(response);
                
                SaveResponseToJsonFile(linkedInClasses);
            } 
            
            catch (System.Exception e)
            
            {
                Debug.Log($"cant deserealise {e}");
            }
            }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    private void SaveResponseToJsonFile(LinkedInClasses jsonResponse)
    {
        try
        {
           
            string json = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
            string path = jsonFilePath;
            File.WriteAllText(path, json);
            Debug.Log("Data saved to: " + path);

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save JSON: {e.Message}");
        }
    }


// necessary to load the data objects into watson?
public LinkedInClasses LoadJsonFile()
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                Debug.Log("Loaded JSON: " + json);
                return JsonConvert.DeserializeObject<LinkedInClasses>(json);
            }
            else
            {
                Debug.LogError("JSON file not found!");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load JSON: {e.Message}");
            return null;
        }
    }
}

