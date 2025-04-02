using System.IO;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{

    [System.Serializable]
    public class ConfigDataModel
    {
        public string linkedinApi;
    }


    private static ConfigDataModel config;

    void Awake()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "config.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            config = JsonUtility.FromJson<ConfigDataModel>(json);
            Debug.Log("api key from config file: " + config.linkedinApi);
        }
        else
        {
            Debug.LogError("Config file not found in config Loader!");
        }
    }

    // can use this for all of the secrets
    public static string GetLinkedinApi()
    {
        return config != null ? config.linkedinApi : "DEFAULT_KEY";
    }
}
