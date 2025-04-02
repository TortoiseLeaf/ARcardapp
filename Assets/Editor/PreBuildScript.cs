#if UNITY_EDITOR
using System.IO;
using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PreBuildScript : IPreprocessBuildWithReport
{

     
    public int callbackOrder => 0; 
    
    public void OnPreprocessBuild(BuildReport report)
    {
        try
        {
            string apiKey = System.Environment.GetEnvironmentVariable("LINKEDIN_API");

        if (!string.IsNullOrEmpty(apiKey))
        {
            string configPath = "Assets/StreamingAssets/config.json";

            string json = $"{{ \"apiKey\": \"{apiKey}\" }}";

            File.WriteAllText(configPath, json);
            Debug.Log("Config file generated at: " + configPath);
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