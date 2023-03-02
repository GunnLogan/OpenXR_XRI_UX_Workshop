using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class CameraFader : MonoBehaviour
{
    [SerializeField]
    private bool fadeInOnStart = true;
    [SerializeField]
    private float speed = 5f;
    private Material material;
    public Action OnCameraFadedIn;
    public Action OnCameraFadedOut;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        if (fadeInOnStart)
        {
            StartFadeIn();
        }
    }

    public void SetColor(Color color)
    {
        var currentColor = material.GetColor("_Color");
        color.a = currentColor.a;
        material.SetColor("_Color", color);
    }

    public void StartFadeOut() => StartFadeOut(speed);
    public void StartFadeIn() => StartFadeIn(speed);

    public void StartFadeOut(float speed)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOut(speed));
    }

    public void StartFadeIn(float speed)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn(speed));
    }

    // TODO not ideal to have two separate coroutines for this, since 
    // this can cause concurrent fading, resulting in unfinished fading.
    private IEnumerator FadeIn(float speed)
    {
        print("Start fade in");
        var c = material.GetColor("_Color");
        while (c.a > 0f)
        {
            c.a -= Time.deltaTime * speed;
            material.SetColor("_Color", c);
            yield return null;
        }
        OnCameraFadedIn?.Invoke();
    }

    private IEnumerator FadeOut(float speed)
    {
        print("Start fade out");
        var c = material.GetColor("_Color");
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * speed;
            material.SetColor("_Color", c);
            yield return null;
        }
        OnCameraFadedOut?.Invoke();
    }
}
