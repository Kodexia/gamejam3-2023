using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPlanetUI : MonoBehaviour
{
    Transform tr;

    [SerializeField]
    Canvas planetUI;
    CanvasGroup planetUIGroup;
    [SerializeField]
    float fadeDuration = 1.0f;
    float elapsedTime = 0.0f;

    Vector2 screenBounds;
    bool fadeIn = false;
    bool fadeOut = false;

    private void Start()
    {
        tr = GetComponent<Transform>();
        planetUIGroup = planetUI.GetComponent<CanvasGroup>();
        planetUI.enabled = false;
        planetUIGroup.alpha = 0f;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        if(fadeIn)
        {
            if(planetUIGroup.alpha < 1)
            {
                float t = elapsedTime / fadeDuration;
                planetUIGroup.alpha = Mathf.Lerp(0f, 1f, t);
                elapsedTime += Time.deltaTime;

                if (planetUIGroup.alpha >= 1 ) 
                {
                    fadeIn = false;
                    elapsedTime = 0.0f;
                }
            }
        }

        else if(fadeOut)
        {
            if(planetUIGroup.alpha > 0)
            {
                float t = elapsedTime / fadeDuration;
                planetUIGroup.alpha = Mathf.Lerp(1f, 0f, t);
                elapsedTime += Time.deltaTime;

                if (planetUIGroup.alpha == 0)
                {
                    fadeOut = false;
                    elapsedTime = 0.0f;
                    planetUI.enabled = false;
                }
            }
        }
    }

    public IEnumerator FadeCanvas(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        canvasGroup.alpha = startAlpha;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    private void OnMouseOver()
    {
        Debug.Log(planetUI.enabled + " - " + fadeIn);
        if (planetUI.enabled == false && fadeIn == false)
        {
            fadeIn = true;
            planetUI.enabled = true;

            planetUI.transform.position = new Vector2(tr.position.x + 1.5f, tr.position.y);
            if (planetUI.transform.position.x + 1 > screenBounds.x)
            {
                planetUI.transform.position = new Vector2(tr.position.x - 1.5f, tr.position.y);

            }

        }
    }

    private void OnMouseExit()
    {
        if(fadeOut == false && planetUI.enabled == true)
        {
            fadeOut = true;
        }
   

    }
}
