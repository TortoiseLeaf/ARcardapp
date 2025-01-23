using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class WatsonTTS : MonoBehaviour
{
    [SerializeField] private string _apiKey = "";
    [SerializeField] private string _apiUrl = "";

    public AudioPlayer audioPlayer;
    void Awake()
    {
        audioPlayer = GetComponent<AudioPlayer>();
        StartCoroutine(SynthesizeAndDownloadAudio());
    }

    IEnumerator SynthesizeAndDownloadAudio()
    {
        // Create a POST request to the Watson TTS API
        UnityWebRequest www = UnityWebRequest.Post(_apiUrl, "{ \"text\": \" Watson test \"}", "application/json");
        www.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("apikey:" + _apiKey)));
        www.SetRequestHeader("Accept", "audio/wav;codec=pcm;rate=44100");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error downloading audio: {www.error}");
        }
        else
        {
            Debug.Log("Downloading audio...");

            byte[] audioData = www.downloadHandler.data;
            
            string audioFilePath = Path.Combine(Application.persistentDataPath, "synthetized_audio.wav");

            // Save the downloaded audio file
            byte[] wavData = AddWavHeader(audioData, 44100, 1, 16);
            File.WriteAllBytes(audioFilePath, wavData);        

            Debug.Log($"Audio saved at {audioFilePath}");

            // Start the coroutine to play the saved audio file
            Debug.Log("Starting PlayAudio coroutine...");
            StartCoroutine(audioPlayer.PlayAudio(audioFilePath));
        }
    }

    private byte[] AddWavHeader(byte[] pcmData, int sampleRate, int channels, int bitsPerSample)
    {
        int byteRate = sampleRate * channels * (bitsPerSample / 8);
        int subchunk2Size = pcmData.Length;
        int chunkSize = 36 + subchunk2Size;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                // RIFF header
                writer.Write(new[] { 'R', 'I', 'F', 'F' }); // Chunk ID
                writer.Write(chunkSize); // Chunk size
                writer.Write(new[] { 'W', 'A', 'V', 'E' }); // Format

                // fmt subchunk
                writer.Write(new[] { 'f', 'm', 't', ' ' }); // Subchunk1 ID
                writer.Write(16); // Subchunk1 size
                writer.Write((short)1); // Audio format (PCM)
                writer.Write((short)channels); // Number of channels
                writer.Write(sampleRate); // Sample rate
                writer.Write(byteRate); // Byte rate
                writer.Write((short)(channels * (bitsPerSample / 8))); // Block align
                writer.Write((short)bitsPerSample); // Bits per sample

                // data subchunk
                writer.Write(new[] { 'd', 'a', 't', 'a' }); // Subchunk2 ID
                writer.Write(subchunk2Size); // Subchunk2 size
                writer.Write(pcmData); // PCM data
            }

            return memoryStream.ToArray();
        }
    }
}
