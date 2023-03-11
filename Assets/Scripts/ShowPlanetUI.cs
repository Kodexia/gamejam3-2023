using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.WSA;

public class ShowPlanetUI : MonoBehaviour
{
    Transform tr;

    Planet planet;
    Canvas planetUI;
    SpriteRenderer rend;
    TextMeshProUGUI planetText;
    CanvasGroup planetUIGroup;
    [SerializeField]
    float fadeDuration = 1.0f;
    float elapsedTime = 0.0f;

    Vector2 screenBounds;
    bool fadeIn = false;
    bool fadeOut = false;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        planet = GetComponent<Planet>();
        GameObject tempObject = GameObject.Find("PlanetUICanvas");
        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            planetUI = tempObject.GetComponent<Canvas>();
            planetUIGroup = tempObject.GetComponent<CanvasGroup>();
            if (planetUI == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }

        GameObject textObject = new GameObject("TextMeshPro", typeof(TextMeshProUGUI));
        textObject.transform.SetParent(planetUI.transform,false);
        planetText = textObject.GetComponent<TextMeshProUGUI>();
        
        planetText.fontSize = 0.3f;
        planetText.alignment = TextAlignmentOptions.Center;
        planetText.horizontalAlignment = HorizontalAlignmentOptions.Center;
        
        
        


        


        tr = GetComponent<Transform>();
        
        

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

    private void OnMouseOver()
    {
        float height = rend.sprite.bounds.size.y;
        float width = rend.sprite.bounds.size.x;
        Debug.Log(planetUI.enabled + " - " + fadeIn);
        
        if (planetUI.enabled == false && fadeIn == false)
        {
            fadeIn = true;
            planetUI.enabled = true;
            planetText.text = $"Planet: {planet.name}\r\nMaterial: {planet.ore.name}\r\nAmmout: {planet.ore.amm}";

            planetUI.transform.position = new Vector2(tr.position.x + height + 1, tr.position.y);
            if (planetUI.transform.position.x + 1 > screenBounds.x)
            {
                planetUI.transform.position = new Vector2(tr.position.x - height - 1, tr.position.y);

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
