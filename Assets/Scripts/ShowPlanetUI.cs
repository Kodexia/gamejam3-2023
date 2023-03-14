using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.WSA;
using UnityEngine.UI;

public class ShowPlanetUI : MonoBehaviour
{
    Transform tr;

    Planet planet;
    Canvas planetUI;
    SpriteRenderer rend;
    TextMeshProUGUI img;
    TextMeshProUGUI planetText;
    [SerializeField]
    Image oreResourceImage;
    [SerializeField]
    Sprite Uranium;
    [SerializeField]
    Sprite Azurite;
    [SerializeField]
    Sprite Crimtain;
    CanvasGroup planetUIGroup;
    [SerializeField]
    float fadeDuration = 1.0f;
    float elapsedTime = 0.0f;

    Vector2 screenBounds;
    bool fadeIn = false;
    bool fadeOut = false;

    private void Start()
    {


        GameObject tempObject = GameObject.Find("PlanetUICanvas");
        GameObject imgTempObject = GameObject.Find("Image (1)");
        if (imgTempObject != null)
        {

            img = imgTempObject.GetComponent<TextMeshProUGUI>();
            if (img == null)
            {
                Debug.Log("didnt find img");
            }
        }
        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            planetUI = tempObject.GetComponent<Canvas>();
            planetUIGroup = tempObject.GetComponent<CanvasGroup>();
            if (planetUI == null)
            {
                //Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }

        if (GameObject.Find("TextMeshPro") == null)
        {
            GameObject textObject = new GameObject("TextMeshPro", typeof(TextMeshProUGUI));
            textObject.transform.SetParent(planetUI.transform, false);
            planetText = textObject.GetComponent<TextMeshProUGUI>();

            planetText.fontSize = 0.3f;
            planetText.alignment = TextAlignmentOptions.Center;
            planetText.horizontalAlignment = HorizontalAlignmentOptions.Center;
        }
        else
        {
            GameObject textObject = GameObject.Find("TextMeshPro");
            textObject.transform.SetParent(planetUI.transform, false);
            planetText = textObject.GetComponent<TextMeshProUGUI>();


            planetText.fontSize = 0.3f;
            planetText.alignment = TextAlignmentOptions.Left;
            planetText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            planetText.rectTransform.sizeDelta = new Vector2(1.9226f, 1.5f);
        }













        planetUI.enabled = false;
        planetUIGroup.alpha = 0f;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);


            if (hit.collider != null && hit.collider.tag == "planet")
            {
                rend = hit.collider.GetComponent<SpriteRenderer>();

                planet = hit.collider.GetComponent<Planet>();
                tr = hit.collider.GetComponent<Transform>();
                ShowPlanetInfo();
            }
            else
            {
                HidePlanetInfo();
            }
        }
        if (fadeIn)
        {
            if (planetUIGroup.alpha < 1)
            {
                float t = elapsedTime / fadeDuration;
                planetUIGroup.alpha = Mathf.Lerp(0f, 1f, t);
                elapsedTime += Time.deltaTime;

                if (planetUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                    elapsedTime = 0.0f;
                }
            }
        }

        else if (fadeOut)
        {
            if (planetUIGroup.alpha > 0)
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

    public void ShowPlanetInfo()
    {
        float height = rend.sprite.bounds.size.y;
        float width = rend.sprite.bounds.size.x;
        //Debug.Log(planetUI.enabled + " - " + fadeIn);

        if (planetUI.enabled == false && fadeIn == false)
        {
            fadeIn = true;
            planetUI.enabled = true;
            planetText.text = $"Planet: {planet.name}\r\nMaterial: {planet.ore.name}\r\nAmmout: {planet.ore.amm}";

            //Vykreslovani obrazku orecek

            if (planet.ore.name == "Uranium")
            {
                oreResourceImage.sprite = Uranium;
            }
            if (planet.ore.name == "Azurite")
            {
                oreResourceImage.sprite = Azurite;
            }
            if (planet.ore.name == "Crimtain")
            {
                oreResourceImage.sprite = Crimtain;
            }

            planetUI.transform.position = new Vector2(tr.position.x + height + 1, tr.position.y);
            if (planetUI.transform.position.x + 1 > screenBounds.x)
            {
                planetUI.transform.position = new Vector2(tr.position.x - height - 1, tr.position.y);

            }



        }
    }

    public void HidePlanetInfo()
    {
        if (fadeOut == false && planetUI.enabled == true)
        {
            fadeOut = true;
        }



    }

}
