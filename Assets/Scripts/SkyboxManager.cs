using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField]
    private Material materialToChangeTo;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = materialToChangeTo;
    }

    public void ChangeSkybox()
    {
        RenderSettings.skybox = materialToChangeTo;
    }
}
