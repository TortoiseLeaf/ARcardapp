using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using System.Collections;

public class SceneTests
{
    // Replace with your scene name
    private const string SceneName = "arc";

    [Test]
    public void SceneExistsInBuildSettings()
    {
        bool sceneExists = false;

        // Check if the scene is in the Build Settings list
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName == SceneName)
            {
                sceneExists = true;
                break;
            }
        }

        Assert.IsTrue(sceneExists, $"Scene '{SceneName}' is not found in the build settings.");
    }

    [UnityTest]
    public IEnumerator SceneLoadsCorrectly()
    {
        // Load the scene asynchronously and check if it's loaded successfully
        var loadScene = SceneManager.LoadSceneAsync(SceneName);

        // Wait until the scene is loaded
        while (!loadScene.isDone)
            yield return null;

        // Check if the scene is loaded and active
        Assert.IsTrue(SceneManager.GetActiveScene().name == SceneName, $"Failed to load scene '{SceneName}'.");
    }
}