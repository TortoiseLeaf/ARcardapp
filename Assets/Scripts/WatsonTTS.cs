using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WatsonTTS : MonoBehaviour
{
    private WatsonCredentials watsonCredentials;
    private string credentialsFilePath;
    public AudioPlayer audioPlayer;

    void Awake()
    {
        credentialsFilePath = Path.Combine(Application.streamingAssetsPath, "credentials.json");

#if UNITY_EDITOR
        PreBuildScript1.GenerateCredsFile();

        LoadCredentials();
        audioPlayer = GetComponent<AudioPlayer>();
#endif

#if UNITY_ANDROID
        PreBuildScript1.GenerateCredsFile();
        Debug.Log("credspath runs in Android watson.");

        StartCoroutine(
                LoadAndroidCreds());
        audioPlayer = GetComponent<AudioPlayer>();
#endif


    }

    private IEnumerator LoadAndroidCreds()
    {
        if (credentialsFilePath.Contains("://") || credentialsFilePath.Contains(":///"))
        {
            Debug.Log("credsloader function in watson web request fires");

            UnityWebRequest www = UnityWebRequest.Get(credentialsFilePath);
            www.SetRequestHeader("Content-Type", "text/plain; charset=utf-8");
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error reading creds in Watson: " + www.error);
            }

            else
            {
                string json = www.downloadHandler.text;
                watsonCredentials = JsonUtility.FromJson<WatsonCredentials>(json);
                Debug.Log("creds watson " + watsonCredentials._watsonApiUrl);
                yield return watsonCredentials;

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
                watsonCredentials = JsonUtility.FromJson<WatsonCredentials>(json);

                Debug.Log("Watson credentials loaded successfully.");
            }
            else
            {
                Debug.LogError("Watson credentials file not found!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Watson Error loading credentials: {e.Message}");
        }
    }

    public bool CheckIsNewWatsonRequest(WatsonRequest request, string path)
    {
        Debug.Log("Watson Checking if request is new...");        
        
        if (File.Exists(path))
        {
            WatsonRequest oldRequest = JsonUtility.FromJson<WatsonRequest>(File.ReadAllText(path));
            if (oldRequest.text == request.text)
            {
                Debug.Log("Watson Requested text is already synthesised.");
                return false;
            }
            else {
                return true;
            }
        } else {
            return true;
        }
    }


    public IEnumerator SynthesizeAndDownloadAudio(WatsonRequest request)
    {
        string requestFileName = $"{request.requestName}LastTTSRequest.json";
        string requestFilePath = Path.Combine(Application.persistentDataPath, requestFileName);
        string audioFileName = $"{request.requestName}.wav";
        string audioFilePath = Path.Combine(Application.persistentDataPath, audioFileName);

        if (CheckIsNewWatsonRequest(request, requestFilePath))
        {
            Debug.Log("Watson New request detected. Synthesizing...");
            string jsonData = JsonUtility.ToJson(request);
            File.WriteAllText(requestFilePath, jsonData);
            Debug.Log($"Watson Request saved at {requestFilePath}");

            byte[] bytes = Encoding.UTF8.GetBytes(jsonData);
            UnityWebRequest www = new UnityWebRequest(watsonCredentials._watsonApiUrl, "POST");
            www.uploadHandler = new UploadHandlerRaw(bytes);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("apikey:" + watsonCredentials._watsonApiKey)));
            www.SetRequestHeader("Accept", "audio/wav;codec=pcm;rate=44100");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Watson Error downloading audio: {www.error}");
                yield break;
            }

            Debug.Log("Watson Downloading audio...");
            byte[] audioData = www.downloadHandler.data;

            // Save the downloaded audio file
            byte[] wavData = CreateWavHeader(audioData, 44100, 1, 16);
            File.WriteAllBytes(audioFilePath, wavData);
            Debug.Log($"Watson Audio saved at {audioFilePath}");
        }
        else
        {
            Debug.Log("Watson Using saved audio..." + audioFilePath);
        }
        

#if UNITY_ANDROID
        Debug.Log("Watson Play audio android");
        StartCoroutine(audioPlayer.PlayAudio(audioFilePath));

#endif

#if UNITY_EDITOR
        if (File.Exists(audioFilePath))
        {
            Debug.Log("Watson Starting PlayAudio coroutine...");
            StartCoroutine(audioPlayer.PlayAudio(audioFilePath));
        }
        else
        {
            Debug.LogError("Watson Audio file not found");
        }
#endif
    }


    public void SynthesizeAndPlayRequest(WatsonRequest request)
    {
        StartCoroutine(SynthesizeAndDownloadAudio(request));
    }

    private byte[] CreateWavHeader(byte[] pcmData, int sampleRate, int channels, int bitsPerSample)
    {
        // Ensure 16-bit PCM format
        if (bitsPerSample != 16)
        {
            Debug.LogWarning("Bits per sample must be 16. Adjusting.");
            bitsPerSample = 16;
        }

        // Calculate various audio parameters
        int byteRate = sampleRate * channels * (bitsPerSample / 8);
        int blockAlign = channels * (bitsPerSample / 8);
        int subchunk2Size = pcmData.Length;
        int chunkSize = 36 + subchunk2Size;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                // RIFF header
                writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(chunkSize);
                writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

                // fmt subchunk
                writer.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));
                writer.Write(16); // Subchunk1 size
                writer.Write((short)1); // Audio format (PCM)
                writer.Write((short)channels);
                writer.Write(sampleRate);
                writer.Write(byteRate);
                writer.Write((short)blockAlign);
                writer.Write((short)bitsPerSample);

                // data subchunk
                writer.Write(System.Text.Encoding.ASCII.GetBytes("data"));
                writer.Write(subchunk2Size);
                writer.Write(pcmData);
            }

            return memoryStream.ToArray();
        }
    }
}
