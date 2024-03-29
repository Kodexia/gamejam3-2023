// original ----
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 5f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    bool holding = false;

    void Start()
    {
        // camera position
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        minX = background.transform.position.x - background.bounds.extents.x + cameraWidth / 2f;
        maxX = background.transform.position.x + background.bounds.extents.x - cameraWidth / 2f;
        minY = background.transform.position.y - background.bounds.extents.y + cameraHeight / 2f;
        maxY = background.transform.position.y + background.bounds.extents.y - cameraHeight / 2f;
    }

    void Update()
    {
        float zoom = Camera.main.orthographicSize;

        // Zoom mouse
        zoom -= Input.mouseScrollDelta.y * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.orthographicSize = zoom;


        //lock the camera to background
        float x = Mathf.Clamp(transform.position.x, minX, maxX);
        float y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(x, y, transform.position.z);

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

            // Lock the camera to background while dragging
            x = Mathf.Clamp(transform.position.x, minX, maxX);
            y = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector3(x, y, transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            holding = false;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraMovement : MonoBehaviour
//{
//    [SerializeField] private SpriteRenderer background;
//    [SerializeField] private Camera planetCamera;
//    [SerializeField] private Camera backgroundCamera;
//    [SerializeField] private float zoomSpeed = 0.1f;
//    [SerializeField] private float minZoom = 4f;
//    [SerializeField] private float maxZoom = 1f;
//    [SerializeField] private float planetsSpeed = 1f;
//    [SerializeField] private float backgroundSpeed = 0.5f;


//    private float minX;
//    private float maxX;
//    private float minY;
//    private float maxY;

//    Vector2 mouseClickPos;
//    Vector2 mouseCurrentPos;
//    bool holding = false;

//    void Start()
//    {
//        // planet camera position
//        float cameraHeight = planetCamera.orthographicSize * 2f;
//        float cameraWidth = cameraHeight * planetCamera.aspect;
//        minX = background.transform.position.x - background.bounds.extents.x + cameraWidth / 2f;
//        maxX = background.transform.position.x + background.bounds.extents.x - cameraWidth / 2f;
//        minY = background.transform.position.y - background.bounds.extents.y + cameraHeight / 2f;
//        maxY = background.transform.position.y + background.bounds.extents.y - cameraHeight / 2f;

//        // background camera position
//       // backgroundCamera.transform.position = new Vector3(background.transform.position.x, background.transform.position.y, backgroundCamera.transform.position.z);
//    }

//    void Update()
//    {
//        float cameraHeight = planetCamera.orthographicSize * 2f;
//        float cameraWidth = cameraHeight * planetCamera.aspect;

//        minX = background.transform.position.x - background.bounds.extents.x + cameraWidth / 2f;
//        maxX = background.transform.position.x + background.bounds.extents.x - cameraWidth / 2f;
//        minY = background.transform.position.y - background.bounds.extents.y + cameraHeight / 2f;
//        maxY = background.transform.position.y + background.bounds.extents.y - cameraHeight / 2f;

//        // zoom planet camera
//        float zoom = planetCamera.orthographicSize - Input.mouseScrollDelta.y * zoomSpeed * planetCamera.orthographicSize;
//        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);

//        planetCamera.orthographicSize = zoom;

//        // lock the planet camera to background
//        float x = Mathf.Clamp(planetCamera.transform.position.x, minX, maxX);
//        float y = Mathf.Clamp(planetCamera.transform.position.y, minY, maxY);
//        planetCamera.transform.position = new Vector3(planetCamera.transform.position.x, planetCamera.transform.position.y, planetCamera.transform.position.z);

//        // move background camera along with the planet camera
//        backgroundCamera.transform.position = new Vector3(planetCamera.transform.position.x * (backgroundSpeed / zoom) - (zoom / 2), planetCamera.transform.position.y * (backgroundSpeed / zoom) - (zoom / 2), backgroundCamera.transform.position.z);

//        if (Input.GetKeyDown(KeyCode.Mouse0) && !holding)
//        {
//            mouseClickPos = planetCamera.ScreenToWorldPoint(Input.mousePosition);
//            holding = true;
//        }

//        if (holding && false)
//        {
//            mouseCurrentPos = planetCamera.ScreenToWorldPoint(Input.mousePosition);
//            var distance = mouseCurrentPos - mouseClickPos;
//            planetCamera.transform.position += new Vector3(-distance.x * planetsSpeed, -distance.y * planetsSpeed, 0);

//            // Lock the planet camera to background while dragging
//            x = Mathf.Clamp(planetCamera.transform.position.x, minX, maxX);
//            y = Mathf.Clamp(planetCamera.transform.position.y, minY, maxY);
//            planetCamera.transform.position = new Vector3(x, y, planetCamera.transform.position.z);


//            //Debug.Log(zoom);

//            backgroundCamera.transform.position = new Vector3(planetCamera.transform.position.x * (backgroundSpeed / zoom) - (zoom / 2), planetCamera.transform.position.y * (backgroundSpeed / zoom) - (zoom / 2), backgroundCamera.transform.position.z);

//        }

//        if (Input.GetKeyUp(KeyCode.Mouse0))
//            holding = false;
//    }
//}
