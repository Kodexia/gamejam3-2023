using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPlanetUI : MonoBehaviour
{
    Transform tr;
    [SerializeField]
    Canvas planetUI;
    Vector2 screenBounds;
    private void Start()
    {
        tr = GetComponent<Transform>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    private void OnMouseOver()
    {
        if (planetUI.enabled == false)
        {

            planetUI.enabled = true;

            planetUI.transform.position = new Vector2(tr.position.x + 1.5f, tr.position.y);
            if (planetUI.transform.position.x + 1 > screenBounds.x)
            {
                planetUI.transform.position = new Vector2(tr.position.x - 1.5f, tr.position.y);

            }

        }

        Debug.Log("in");
    }
    private void OnMouseExit()
    {
        if(planetUI.enabled == true)
        {

            planetUI.enabled = false;
        }
   

    }
}
