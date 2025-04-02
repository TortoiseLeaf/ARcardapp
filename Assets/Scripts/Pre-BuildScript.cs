using System.IO;
using UnityEditor;
using UnityEngine;

public class PreBuildConfig
{
    [MenuItem("Build/Generate Config File")]
    public static void CreateConfig()
    {
        string _linkedinApi = System.Environment.GetEnvironmentVariable("LINKEDIN_API");

        if (!string.IsNullOrEmpty(_linkedinApi))
        {
            string configFilePath = "Assets/StreamingAssets/config.json";
            File.WriteAllText(configFilePath, $"{{ \"_linkedinApi\": \"{_linkedinApi}\" }}");
            Debug.Log("Config file generated: " + configFilePath);
        }
    }
}