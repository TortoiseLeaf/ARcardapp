using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WatsonTTS : MonoBehaviour
{
    [SerializeField] private string _apiKey = "";
    [SerializeField] private string _apiUrl = "";
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        UnityWebRequest www = UnityWebRequest.Post(_apiUrl, "{ \"text\": \" Watson test \"}", "application/json");        
        
        www.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("apikey:" + _apiKey)));
        www.SetRequestHeader("Accept", "audio/wav");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
