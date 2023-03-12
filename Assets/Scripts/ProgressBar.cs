using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float maximum;
    public float progress;
    public Image mask;
    public Image fill;
    public Color color;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        float fillAmount = (float)progress / (float)maximum;
        mask.fillAmount = fillAmount;
        fill.color = color;

    }
}
