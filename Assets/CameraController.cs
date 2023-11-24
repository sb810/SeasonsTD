using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Button centerCamButton;
    private PixelPerfectCamera ppcam;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
        ppcam = GetComponent<PixelPerfectCamera>();
    }

    private void Update()
    {
        if (!followTarget) return;
        Vector3 pos = transform.position;
        Vector3 targetPos = followTarget.position;
        transform.position = Vector3.Lerp(new Vector3(pos.x, pos.y, -10), new Vector3(targetPos.x, targetPos.y, -10), 0.02f);;
    }

    public void SetFollowTarget(Transform t)
    {
        followTarget = t;
    }

    public void UpdatePosition(BaseEventData data)
    {
        followTarget = null;
        centerCamButton.interactable = true;
        PointerEventData pointerData = (PointerEventData)data;
        // Vector3 delta = cam.ScreenToViewportPoint(new Vector3(pointerData.delta.x, pointerData.delta.y, 0));
        transform.position += new Vector3(-pointerData.delta.x/100, -pointerData.delta.y/100, 0);

    }
}
