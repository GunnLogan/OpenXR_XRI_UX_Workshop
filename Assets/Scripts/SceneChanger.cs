using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class SceneChanger : MonoBehaviour
{
    
    private string sceneName;
    private Transform mCamera;
    private CameraFader cameraFader;

    private void Awake()
    {
        cameraFader = FindObjectOfType<CameraFader>();
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        Assert.IsNotNull(cameraFader, "No camera fader found in scene");
        Assert.IsNotNull(mCamera, "No main camera found in scene");
    }

    public void LoadSelectedScene(string _sceneToLoad)
    {
        sceneName = _sceneToLoad;
        StartCoroutine(LoadAsyncSelectedScene());
    }

    IEnumerator LoadAsyncSelectedScene()
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        cameraFader.StartFadeOut(1.5f);

        //Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
