using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class CredsLoader : MonoBehaviour
{
    private static ProxycurlCredentials proxycurlCredentials;
    private static WatsonCredentials watsonCredentials;

    private static bool isLoaded = false;

    public static event Action OnCredsLoaded;

    private void Awake()
    {
        // Start async loading
        StartCoroutine(LoadProdCreds());
    }

    private IEnumerator LoadProdCreds()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "credentials.json");

        UnityWebRequest request = new UnityWebRequest(path);
        request.downloadHandler = new DownloadHandlerBuffer();

#if UNITY_ANDROID && !UNITY_EDITOR
        // On Android, need to use URI format
        path = path.Replace("jar:file://", "jar:file:///");
#endif

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            proxycurlCredentials = JsonUtility.FromJson<ProxycurlCredentials>(json);
            watsonCredentials = JsonUtility.FromJson<WatsonCredentials>(json);
            isLoaded = true;
            Debug.Log("Creds Loaded: linkedin = " + proxycurlCredentials._baseUrlLinkedIn);
            Debug.Log("Creds Loaded: watson = " + watsonCredentials._watsonApiUrl);


            // Notify listeners
            OnCredsLoaded?.Invoke();
        }
        else
        {
            Debug.LogError("Failed to load creds: " + request.error);
        }
    }

    public static ProxycurlCredentials GetLinkedInProdCreds()
    {
        return isLoaded && proxycurlCredentials != null ? proxycurlCredentials : new ProxycurlCredentials();
    }

    public static WatsonCredentials GetWatsonProdCreds()
    {
        return isLoaded && watsonCredentials != null ? watsonCredentials : new WatsonCredentials();
    }

    // check if config has loaded
    public static bool AreCredsLoaded()
    {
        return isLoaded;
    }
}
