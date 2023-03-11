using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool holding = false;

    void Update()
    {
        // To-Do: Udìlat locknutí na background, aby se camera nemohla pohnout za background

        if (Input.GetKeyDown(KeyCode.Mouse0) && !holding)
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            holding = true;
        }

        if (holding)
        {
            mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var distance = mouseCurrentPos - mouseClickPos;
            transform.position += new Vector3(-distance.x, -distance.y, 0);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            holding = false;
    }
}
