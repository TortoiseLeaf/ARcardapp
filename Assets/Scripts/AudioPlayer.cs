using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    
/*     void Start() 
    {           
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }        
    } */

     void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    } 
    public IEnumerator PlayAudio(string filePath)
    {
        Debug.Log("Entering PlayAudio coroutine...");
        
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File does not exist at path: {filePath}");
            yield break;
        }

        Debug.Log($"Loading audio from: {filePath}");
        string url = "file://" + filePath;
        
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error loading audio: {www.error}");
            yield break;
        }

        Debug.Log("Audio loaded successfully!");

       
        AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);

        if (audioClip == null)
        {
            Debug.LogError("Failed to create AudioClip from WAV data.");
            yield break;
        }
        
        Debug.Log("playing the clip...");        
        audioSource.clip = audioClip;
        audioSource.Play();        
    }
}
