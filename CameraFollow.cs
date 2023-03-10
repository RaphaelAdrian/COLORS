using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Range(0,0.1f)]
    public float speed = 0.03f;

    public Transform targetObject;
    public Vector2 offset = Vector2.zero;

    [Header("Mouse Controls")]
    [Range(0.1f, 5f)]
    public float zoomSpeed = 2f;
    public float maxZoom = 15f;
    public float minZoom = 2.5f;
    float targetZoom;
    Vector3 targetPos;
    Vector2 initMousePos;
    Vector2 initOffset;
    Camera cam;

    bool isMousePressed;
    bool isMouseLocked;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        initOffset = offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetObject)
            return;

        HandleCameraZoom();
        HandleCameraOffset();

        if (transform.position != targetPos) {
            if (!isMousePressed)
                transform.position = Vector3.Lerp(transform.position, targetPos, speed);
            else
                transform.position = targetPos;
            
        }
        
        if (targetZoom != cam.orthographicSize) {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, speed);
        }
    }

    private void HandleCameraOffset()
    {
        if (Input.GetMouseButtonDown(0)) {
            initMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            isMousePressed = true;
        }
        if (Input.GetMouseButton(0)) {
            Vector2 travelPos = cam.ScreenToWorldPoint(Input.mousePosition);
            offset += (initMousePos - travelPos);
        }
        if (Input.GetMouseButtonUp(0)) {
            isMousePressed = false;
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            offset = initOffset;
        }

        targetPos = new Vector3(
            targetObject.position.x + offset.x, 
            targetObject.position.y + offset.y, transform.position.z);
    }

    private void HandleCameraZoom()
    {
        float mouseScrollY = Input.mouseScrollDelta.y;
        if (mouseScrollY == 0) return;

        targetZoom = cam.orthographicSize - (mouseScrollY * zoomSpeed);
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
    }
}
