#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PreBuildConfigInjector
{
    public static void GenerateCredsFile()
    {
        try
        {
            string _apiKeyLinkedIn = System.Environment.GetEnvironmentVariable("LINKEDIN_API");
            string _baseUrlLinkedIn = System.Environment.GetEnvironmentVariable("LINKEDIN_BASE_URL");
            string _linkedinProfileUrl = System.Environment.GetEnvironmentVariable("LINKEDIN_PROFILE");
            string _watsonApiKey = System.Environment.GetEnvironmentVariable("WATSON_API");
            string _watsonApiUrl = System.Environment.GetEnvironmentVariable("WATSON_URL");

            if (!string.IsNullOrEmpty(_apiKeyLinkedIn))
            {
                string credsPath = "Assets/StreamingAssets/credentials.json";

                string json = $"{{ \"_apiKeyLinkedIn\": \"{_apiKeyLinkedIn}\" }}" +
                        $"{{ \"_baseUrlLinkedIn\": \"{_baseUrlLinkedIn}\" }}" +
                        $"{{ \"_linkedinProfileUrl\": \"{_linkedinProfileUrl}\" }}" +
                        $"{{ \"_watsonApiKey\": \"{_watsonApiKey}\" }}" +
                        $"{{ \"_watsonApiUrl\": \"{_watsonApiUrl}\" }}";

                File.WriteAllText(credsPath, json);
                Debug.Log("Config file generated at: " + credsPath);
            }
            else
            {
                Debug.LogError("cannot find LINKEDIN_API in environment!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("failed Prebuildscript: " + e);
        }
    }
}
#endif