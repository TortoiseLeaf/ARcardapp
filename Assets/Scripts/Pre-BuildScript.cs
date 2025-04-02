using System.IO;
using System;
using UnityEngine;

public class PreBuildConfig
{
    public static void CreateConfig()
    {
        try
        {
            string _linkedinApi = System.Environment.GetEnvironmentVariable("LINKEDIN_API");

            if (!string.IsNullOrEmpty(_linkedinApi))
            {
                string configFilePath = "Assets/StreamingAssets/config.json";
                File.WriteAllText(configFilePath, $"{{ \"_linkedinApi\": \"{_linkedinApi}\" }}");
                Debug.Log("Config file generated: " + configFilePath);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error generating config file: " + e);
        }
    }
}