using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCamera : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void UpdatePosition(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
       // Vector3 delta = cam.ScreenToViewportPoint(new Vector3(pointerData.delta.x, pointerData.delta.y, 0));
        transform.position += new Vector3(-pointerData.delta.x/100, -pointerData.delta.y/100, 0);
    }
}
