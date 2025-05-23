using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class LinkedInAPI : MonoBehaviour
{

    private ProxycurlCredentials credentials;
    private string credentialsFilePath;
    
    private string jsonFilePath;

    void Awake()
    {

#if UNITY_EDITOR
        Debug.Log("runs in Editor");
        credentialsFilePath = Path.Combine(Application.streamingAssetsPath, "credentials.json");
        LoadCredentials();
        jsonFilePath = Path.Combine(Application.persistentDataPath, "LinkedInProfile.json");
#endif

#if UNITY_ANDROID
        credentialsFilePath = Path.Combine(Application.streamingAssetsPath, "credentials.json");
        Debug.Log("credspath runs in Android.");
        StartCoroutine(
                LoadCredsAndroid());
        jsonFilePath = Path.Combine(Application.persistentDataPath, "LinkedInProfile.json");
#endif

        

    }


    private IEnumerator LoadCredsAndroid()
    {
        if (credentialsFilePath.Contains("://") || credentialsFilePath.Contains(":///"))
        {
            Debug.Log("credsloader function in linkeeidn web request fires");

            UnityWebRequest www = UnityWebRequest.Get(credentialsFilePath);
            www.SetRequestHeader("Content-Type", "text/plain; charset=utf-8");
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error reading creds in LinkedinAPI: " + www.error);
            }

            else
            {
                string json = www.downloadHandler.text;
                credentials = JsonUtility.FromJson<ProxycurlCredentials>(json);
                Debug.Log("creds loaded android linkedin: " + credentials._baseUrlLinkedIn);

                yield return credentials;

            }
        }
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
        Debug.Log("creds try in fecthLinkedinProfile: " + credentials._baseUrlLinkedIn);

        if (credentials._apiKeyLinkedIn == null || string.IsNullOrEmpty(credentials._apiKeyLinkedIn))
        {
            Debug.LogError("creds API credentials are not set!");
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


    // this worked in android so why not prod?
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

